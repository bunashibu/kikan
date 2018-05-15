using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingCanceler : MonoBehaviour {
    void Start() {
      _cancelButton.onClick.AddListener( () => Cancel() );
    }

    private void Cancel() {
      RemoveApplyingPlayer();
      _board.SetApplyMode();
    }

    private void RemoveApplyingPlayer() {
      string propKey = "Applying" + _applier.CurApplyingType;

      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      playerNameList.Remove(PhotonNetwork.player.NickName);

      var props = new Hashtable() {{ propKey, playerNameList.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    [SerializeField] private MatchingApplier _applier;
    [SerializeField] private MatchingBoard _board;
    [SerializeField] private Button _cancelButton;
  }
}

