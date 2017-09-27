using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Bunashibu.Kikan {
  public class LobbyPhotonManager : Photon.PunBehaviour {
    void Start() {
      var currentScene = SceneManager.GetSceneByName("Lobby");
      MonoUtility.Instance.DelayUntil(() => currentScene == SceneManager.GetActiveScene(), () => {
        PhotonNetwork.Instantiate("Prefabs/Job/Common", new Vector3(0, 0, 0), Quaternion.identity, 0);
      });
    }
  
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

