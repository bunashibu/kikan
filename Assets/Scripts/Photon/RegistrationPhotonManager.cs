using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationPhotonManager : Photon.PunBehaviour {
  void Awake() {
    PhotonNetwork.logLevel = _logLevel;
    PhotonNetwork.autoJoinLobby = false;
    PhotonNetwork.automaticallySyncScene = true;
  }

  void Start() {
    _controlPanel.SetActive(true);
    _progressLabel.SetActive(false);
  }

  public void Connect() {
    _isConnecting = true;

    _controlPanel.SetActive(false);
    _progressLabel.SetActive(true);

    if (PhotonNetwork.connected)
      PhotonNetwork.JoinRandomRoom();
    else
      PhotonNetwork.ConnectUsingSettings(_gameVersion);
  }

  public override void OnConnectedToMaster() {
    Debug.Log("OnConnectedToMaster() was called");

    if (_isConnecting)
      PhotonNetwork.JoinRandomRoom();
  }

  public override void OnDisconnectedFromPhoton() {
    Debug.Log("OnDisconnectedFromPhoton() was called");
  }

  public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
    Debug.Log("OnPhotonRandomJoinFailed() was called");
    PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = _maxPlayers }, null);
  }

  public override void OnJoinedRoom() {
    Debug.Log("OnJoinedRoom() was called");
    _sceneChanger.ChangeScene(_nextSceneName);
  }

  [SerializeField] private PhotonLogLevel _logLevel;
  [SerializeField] private GameObject _controlPanel;
  [SerializeField] private GameObject _progressLabel;
  [SerializeField] private SceneChanger _sceneChanger;
  [SerializeField] private byte _maxPlayers;
  [SerializeField] private string _nextSceneName;
  private bool _isConnecting;
  private string _gameVersion = "v0.1";
}

