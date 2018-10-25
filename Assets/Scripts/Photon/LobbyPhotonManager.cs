using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  // Useless
  public class LobbyPhotonManager : Photon.PunBehaviour {
    public override void OnPhotonPlayerConnected(PhotonPlayer other) {
      Debug.Log("OnPhotonPlayerConnected() was called" + other.NickName);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other) {
      Debug.Log("OnPhotonPlayerDisconnected() was called" + other.NickName);
    }

    public override void OnLeftRoom() {
      if (_logoutFlag) {
        Debug.Log("OnLeftRoom() was called");
        _logoutFlag = false;
        _sceneChanger.ChangeScene(_nextSceneName);
      }
    }

    public void Logout() {
      Debug.Log("Logout() was called");
      _logoutFlag = true;
      _nextSceneName = "Registration";
      PhotonNetwork.LeaveRoom();
    }

    [SerializeField] private SceneChanger _sceneChanger;
    private string _nextSceneName;
    private bool _logoutFlag;
  }
}

