using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class RegistrationPhotonManager : Photon.PunBehaviour {
    void Awake() {
      PhotonNetwork.logLevel = _logLevel;
      PhotonNetwork.autoJoinLobby = false;
      PhotonNetwork.automaticallySyncScene = false;
      PhotonNetwork.sendRate = 30;
      PhotonNetwork.sendRateOnSerialize = 30;

      Screen.SetResolution(1366, 768, false);
    }

    void Start() {
      _controlPanel.SetActive(true);
      _progressLabel.SetActive(false);
      _errorLabel.SetActive(false);
    }

    public void Connect() {
      _isConnecting = true;
      _tryCount = 0;

      _controlPanel.SetActive(false);
      _progressLabel.SetActive(true);
      _errorLabel.SetActive(false);

      if (PhotonNetwork.connected)
        PhotonNetwork.JoinRoom("Lobby");
      else
        PhotonNetwork.ConnectUsingSettings(GameData.Instance.GameVersion);
    }

    public override void OnConnectedToMaster() {
      Debug.Log("OnConnectedToMaster() was called");

      _tryCount += 1;

      if (_tryCount >= 3) {
        Disconnect();
        return;
      }

      if (_isConnecting)
        PhotonNetwork.JoinRoom("Lobby");
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      Debug.Log("OnPhotonJoinRoomFailed() was called");

      RoomOptions roomOptions = new RoomOptions();
      roomOptions.MaxPlayers = _maxPlayers;

      PhotonNetwork.CreateRoom("Lobby", roomOptions, null);
    }

    public override void OnJoinedRoom() {
      Debug.Log("OnJoinedRoom() was called");
      _setPlayerName.SetName();
      SceneChanger.Instance.ChangeSceneWithSE(_nextSceneName, _source, _clip);
    }

    private void Disconnect() {
      _controlPanel.SetActive(true);
      _progressLabel.SetActive(false);
      _errorLabel.SetActive(true);
    }

    [SerializeField] private PhotonLogLevel _logLevel;
    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _errorLabel;
    [SerializeField] private SetPlayerName _setPlayerName; // dirty
    [SerializeField] private byte _maxPlayers;

    [SerializeField] private string _nextSceneName;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    private bool _isConnecting;

    private int _tryCount;
  }
}

