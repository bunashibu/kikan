using System.Collections;
using System.Collections.Generic;
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
        { ApplyType.VS3,      6 },
      };
    }

    [PunRPC]
    public void ApproveRPC(PhotonPlayer player, ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      Add(player, applyType);
      GiveApplyingTicket(player, applyType);

      if (_applyingCount == _matchCount[applyType])
        _launcher.StartBattle(_matchCount[applyType]);
    }

    private void GiveApplyingTicket(PhotonPlayer player, ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var props = new Hashtable() {{ "ApplyingTicket", (int)applyType }};
      player.SetCustomProperties(props);
    }

    private void Add(PhotonPlayer player, ApplyType applyType) {
      string propKey = "Applying" + applyType;
      var updateType = CustomPropertyUpdateType.Add;

      UpdateRoomCustomProperties(updateType, propKey, player.NickName);
    }

    [PunRPC]
    public void CancelRPC(PhotonPlayer player) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var applyingTicket = (int)player.CustomProperties["ApplyingTicket"];

      Remove(player, (ApplyType)applyingTicket);
      DeleteApplyingTicket(player);
    }

    private void DeleteApplyingTicket(PhotonPlayer player) {
      var props = new Hashtable() {{ "ApplyingTicket", "" }};
      player.SetCustomProperties(props);
    }

    private void Remove(PhotonPlayer player, ApplyType applyType) {
      string propKey = "Applying" + applyType;
      var updateType = CustomPropertyUpdateType.Remove;

      UpdateRoomCustomProperties(updateType, propKey, player.NickName);
    }

    // NOTE: This method is not for general purpose.
    // I probably think something like that CustomPropety-Manager-Class should be created.
    private void UpdateRoomCustomProperties(CustomPropertyUpdateType updateType, string propKey, string propValue) {
      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      switch (updateType) {
        case CustomPropertyUpdateType.Add:
          playerNameList.Add(propValue);
          break;
        case CustomPropertyUpdateType.Remove:
          playerNameList.Remove(propValue);
          break;
      }
      _applyingCount = playerNameList.Count;

      var props = new Hashtable() {{ propKey, playerNameList.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    public Dictionary<ApplyType, int> MatchCount => _matchCount;

    private enum CustomPropertyUpdateType {
      Add,
      Remove
    }

    [SerializeField] private BattleLauncher _launcher;
    [SerializeField] private Dictionary<ApplyType, int> _matchCount;
    private int _applyingCount;
  }
}

