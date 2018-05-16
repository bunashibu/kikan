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
      photonView.RPC("CancelRequestRPC", PhotonTargets.All, PhotonNetwork.player);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      /*
      if (list.Contains(player.NickName))
        RemoveApplyingPlayer(player);
        */
    }

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private Button _cancelButton;
  }
}

