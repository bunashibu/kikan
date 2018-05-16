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

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      /*
      if (list.Contains(player.NickName))
        RemoveApplyingPlayer(player);
        */
    }

    private void Cancel() {
      _board.SetApplyMode();
      RemoveApplyingPlayer();
      RemoveApplyingType();
    }

    private void RemoveApplyingPlayer() {
      var applyingType = (int)PhotonNetwork.player.CustomProperties["ApplyingType"];
      string propKey = "Applying" + applyingType.ToString();

      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      playerNameList.Remove(PhotonNetwork.player.NickName);

      var props = new Hashtable() {{ propKey, playerNameList.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    private void RemoveApplyingType() {
      var props = new Hashtable() {{ "ApplyingType", "" }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    [SerializeField] private MatchingApplier _applier;
    [SerializeField] private MatchingBoard _board;
    [SerializeField] private Button _cancelButton;
  }
}

