using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingApprover : Photon.MonoBehaviour {
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
      photonView.RPC("GetApplyingTicketRPC", PhotonTargets.All, player.ID, applyType);

      if (_applyingCount == _matchCount[applyType])
        _launcher.StartBattle(_matchCount[applyType]);
    }

    private void Add(PhotonPlayer player, ApplyType applyType) {
      string propKey = "Applying" + applyType;

      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      playerNameList.Add(player.NickName);
      _applyingCount = playerNameList.Count;

      var props = new Hashtable() {{ propKey, playerNameList.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    public Dictionary<ApplyType, int> MatchCount => _matchCount;

    [SerializeField] private BattleLauncher _launcher;
    [SerializeField] private Dictionary<ApplyType, int> _matchCount;
    private int _applyingCount;
  }
}

