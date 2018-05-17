using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingMediator : Photon.MonoBehaviour {
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
      GiveApplyingTicket(player, applyType);
      _applicantList[applyType].Add(player);

      if (_applicantList[applyType].Contains(PhotonNetwork.player))
        _board.UpdateNameBoard();

      if (_applicantList[applyType].Count == _matchCount[applyType])
        _launcher.StartBattle(_matchCount[applyType]);
    }

    private void GiveApplyingTicket(PhotonPlayer player, ApplyType applyType) {
      var props = new Hashtable() {{ "ApplyingTicket", (int)applyType }};
      player.SetCustomProperties(props);
    }

    [PunRPC]
    public void CancelRPC(PhotonPlayer player) {
      var applyType = (ApplyType)player.CustomProperties["ApplyingTicket"];
      _applicantList[applyType].Remove(player);

      if (_applicantList[applyType].Contains(PhotonNetwork.player))
        _board.UpdateNameBoard();

      DeleteApplyingTicket();
    }

    private void DeleteApplyingTicket() {
      var props = new Hashtable() {{ "ApplyingTicket", "" }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    /*
    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
    }
    */

    public Dictionary<ApplyType, int> MatchCount => _matchCount;
    public Dictionary<ApplyType, List<PhotonPlayer>> ApplicantList => _applicantList;

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private BattleLauncher _launcher;
    private Dictionary<ApplyType, int> _matchCount;
    private Dictionary<ApplyType, List<PhotonPlayer>> _applicantList;
  }
}

