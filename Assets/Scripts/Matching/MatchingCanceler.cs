using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingCanceler : Photon.PunBehaviour {
    void Start() {
      _cancelButton.onClick.AddListener( () => Cancel() );
    }

    private void Cancel() {
      _board.SetApplyMode();
      photonView.RPC("CancelRPC", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private Button _cancelButton;
  }
}

