using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class LobbyPlayerInstantiator : MonoBehaviour {
    void Awake() {
      var currentScene = SceneManager.GetSceneByName("Lobby");
      MonoUtility.Instance.DelayUntil(() => currentScene == SceneManager.GetActiveScene(), () => {
        InstantiatePlayer();
      });
    }

    private void InstantiatePlayer() {
      var player = PhotonNetwork.Instantiate("Prefabs/Job/Common", new Vector3(0, 0, 0), Quaternion.identity, 0).GetComponent<BattlePlayer>() as BattlePlayer;
      player.Movement.SetMoveForce(4.0f);
      player.Movement.SetJumpForce(400.0f);
      SetViewID(player);
    }

    private void SetViewID(BattlePlayer player) {
      var viewID = player.PhotonView.viewID;

      var props = new Hashtable() {{"ViewID", viewID}};
      PhotonNetwork.player.SetCustomProperties(props);
    }
  }
}

