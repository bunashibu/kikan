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
      _pendingList = new List<Applicant>();
    }

    [PunRPC]
    public void ApproveRequestRPC(PhotonPlayer player, ApplyType applyType) {
      var applicant = new Applicant() {
        player = player,
        applyType = applyType
      };
      _pendingList.Add(applicant);

      if (PhotonNetwork.isMasterClient)
        Approve(applicant);
    }

    private void Approve(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      GiveApplyingTicket(applicant);
      Add(applicant);

      photonView.RPC("ApprovedRPC", PhotonTargets.All, applicant.player);

      if (_approvedApplicantCount == _matchCount[applicant.applyType])
        _launcher.StartBattle(_matchCount[applicant.applyType]);
    }

    private void GiveApplyingTicket(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var props = new Hashtable() {{ "ApplyingTicket", applicant.applyTicket }};
      applicant.player.SetCustomProperties(props);
    }

    private void Add(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      string propKey = "Applying" + applicant.applyTicket;
      var updateType = CustomPropertyUpdateType.Add;

      UpdateRoomCustomProperties(updateType, propKey, applicant.player.NickName);
    }

    [PunRPC]
    public void ApprovedRPC(PhotonPlayer approvedPlayer) {
      _pendingList.Where(applicant => applicant.player != approvedPlayer);

      if (PhotonNetwork.player == approvedPlayer)
        _board.SetMatchWaitingMode();
    }

    [PunRPC]
    public void CancelRequestRPC(PhotonPlayer player) {
      var cancelApplicant = _pendingList.Where(applicant => applicant.player == player).First();
      _pendingList.Remove(cancelApplicant);

      if (PhotonNetwork.isMasterClient)
        Cancel(cancelApplicant);
    }

    private void Cancel(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      DeleteApplyingTicket(applicant);
      Remove(applicant);
    }

    private void DeleteApplyingTicket(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var props = new Hashtable() {{ "ApplyingTicket", "" }};
      applicant.player.SetCustomProperties(props);
    }

    private void Remove(Applicant applicant) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      string propKey = "Applying" + applicant.applyTicket;
      var updateType = CustomPropertyUpdateType.Remove;

      UpdateRoomCustomProperties(updateType, propKey, applicant.player.NickName);
    }

    // NOTE: This method is not for general purpose.
    // I probably think something like that CustomPropety-Manager-Class should be created.
    private void UpdateRoomCustomProperties(CustomPropertyUpdateType updateType, string propKey, string propValue) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      switch (updateType) {
        case CustomPropertyUpdateType.Add:
          if (playerNameList.Contains(propValue)) {
            Debug.Log("Already exists");
            return;
          }
          playerNameList.Add(propValue);
          break;
        case CustomPropertyUpdateType.Remove:
          playerNameList.Remove(propValue);
          break;
      }
      _approvedApplicantCount = playerNameList.Count;

      var props = new Hashtable() {{ propKey, playerNameList.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    public Dictionary<ApplyType, int> MatchCount => _matchCount;

    private enum CustomPropertyUpdateType {
      Add,
      Remove
    }

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private BattleLauncher _launcher;
    private Dictionary<ApplyType, int> _matchCount;
    private List<Applicant> _pendingList;
    private int _approvedApplicantCount;
  }

  public class Applicant {
    public PhotonPlayer player;
    public ApplyType applyType;
    public int applyTicket => (int)applyType;
  }
}

