using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingApplier : Photon.MonoBehaviour {
    void Start() {
      _applyButtonList[0].onClick.AddListener( () => Apply(ApplyType.Practice) );
      _applyButtonList[1].onClick.AddListener( () => Apply(ApplyType.VS1)      );
      _applyButtonList[2].onClick.AddListener( () => Apply(ApplyType.VS2)      );
      _applyButtonList[3].onClick.AddListener( () => Apply(ApplyType.VS3)      );
    }

    private void Apply(ApplyType applyType) {
      _board.SetApproveWaitingMode();
      photonView.RPC("ApproveRPC", PhotonTargets.MasterClient, PhotonNetwork.player, applyType);
    }

    [PunRPC]
    public void GetApplyingTicketRPC(int playerID, ApplyType applyType) {
      if (PhotonNetwork.player.ID == playerID) {
        var props = new Hashtable() {{ "ApplyingType", (int)applyType }};
        PhotonNetwork.player.SetCustomProperties(props);
      }
    }

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private List<Button> _applyButtonList;
  }

  public enum ApplyType {
    Practice = 0,
    VS1 = 1,
    VS2 = 2,
    VS3 = 3
  }
}

