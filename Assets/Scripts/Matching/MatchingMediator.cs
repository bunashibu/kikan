using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingMediator : Photon.PunBehaviour {
    void Start() {
      _matchCount = new Dictionary<ApplyType, int>() {
        { ApplyType.Practice, 1 },
        { ApplyType.VS1,      2 },
        { ApplyType.VS2,      4 },
        { ApplyType.VS3,      6 }
      };
      _applicantList = new Dictionary<ApplyType, List<PhotonPlayer>>() {
        { ApplyType.Practice, new List<PhotonPlayer>() },
        { ApplyType.VS1,      new List<PhotonPlayer>() },
        { ApplyType.VS2,      new List<PhotonPlayer>() },
        { ApplyType.VS3,      new List<PhotonPlayer>() }
      };
    }

    [PunRPC]
    public void ApproveRPC(PhotonPlayer player, ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      GiveApplyingTicket(player, applyType);
      _applicantList[applyType].Add(player);

      photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.All, _applicantList[applyType].ToArray(), applyType);

      if (_applicantList[applyType].Count == _matchCount[applyType])
        _launcher.StartBattle(_matchCount[applyType]);
    }

    private void GiveApplyingTicket(PhotonPlayer player, ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var props = new Hashtable() {{ "ApplyingTicket", (int)applyType }};
      player.SetCustomProperties(props);
    }

    [PunRPC]
    public void CancelRPC(PhotonPlayer player) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var applyType = (ApplyType)player.CustomProperties["ApplyingTicket"];
      _applicantList[applyType].Remove(player);

      photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.All, _applicantList[applyType].ToArray(), applyType);

      DeleteApplyingTicket();
    }

    private void DeleteApplyingTicket() {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var props = new Hashtable() {{ "ApplyingTicket", "" }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
      if (PhotonNetwork.isMasterClient) {
        photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.Others, _applicantList[ApplyType.VS1].ToArray(), ApplyType.VS1);
        photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.Others, _applicantList[ApplyType.VS2].ToArray(), ApplyType.VS2);
        photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.Others, _applicantList[ApplyType.VS3].ToArray(), ApplyType.VS3);
      }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      if (PhotonNetwork.isMasterClient) {
        var tmp = player.CustomProperties["ApplyingTicket"];
        if (tmp == null)
          return;

        var applyType = (ApplyType)tmp;
        _applicantList[applyType].Remove(player);

        photonView.RPC("SyncOneApplicantListRPC", PhotonTargets.All, _applicantList[applyType].ToArray(), applyType);
      }
    }

    [PunRPC]
    private void SyncOneApplicantListRPC(PhotonPlayer[] playerAry, ApplyType applyType) {
      _applicantList[applyType] = MonoUtility.ToList<PhotonPlayer>(playerAry);

      if (_applicantList[applyType].Contains(PhotonNetwork.player))
        _board.UpdateNameBoard();
    }

    public Dictionary<ApplyType, int> MatchCount => _matchCount;
    public Dictionary<ApplyType, List<PhotonPlayer>> ApplicantList => _applicantList;

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private BattleLauncher _launcher;
    private Dictionary<ApplyType, int> _matchCount;
    private Dictionary<ApplyType, List<PhotonPlayer>> _applicantList;
  }
}

