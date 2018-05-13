using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class WinLoseJudger : Photon.PunBehaviour {
    void Start() {
      InitAlivePlayerCount();
      MonoUtility.Instance.DelayUntil(() => _timePanel.TimeSec <= 0, () => {
        int[] alivePlayerCount = PhotonNetwork.room.CustomProperties["AlivePlayerCount"] as int[];
        int redCount = alivePlayerCount[0];
        int blueCount = alivePlayerCount[1];

        if ( (redCount > 0) && (blueCount > 0) )
          ShowDraw();
      });
    }

    public void SetTimePanel(TimePanel timePanel) {
      _timePanel = timePanel;
    }

    public void SetCamera(TrackCamera camera) {
      _camera = camera;
    }

    public void SetCanvas(Canvas canvas) {
      _canvas = canvas;
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable props) {
      if (_isFinished)
        return;

      int[] alivePlayerCount = props["AlivePlayerCount"] as int[];
      int redCount = alivePlayerCount[0];
      int blueCount = alivePlayerCount[1];

      if ( (redCount > 0) && (blueCount > 0) )
        return;

      int team = (int)PhotonNetwork.player.CustomProperties["Team"];

      if ( (redCount == 0 && team == 0) || (blueCount == 0 && team == 1) )
        ShowLose();
      else
        ShowWin();
    }

    public override void OnConnectedToMaster() {
      if (_isFinished)
        PhotonNetwork.JoinRoom("Lobby");
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      if (_isFinished) {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)20;

        PhotonNetwork.JoinOrCreateRoom("Lobby", roomOptions, null);
      }
    }

    public override void OnJoinedRoom() {
      if (_isFinished)
        SceneChanger.Instance.ChangeScene("Lobby");
    }

    public override void OnLeftRoom() {
      if (_isFinished) {
        if (PhotonNetwork.connected)
          PhotonNetwork.JoinRoom("Lobby");
        else
        PhotonNetwork.ConnectUsingSettings(GameData.Instance.GameVersion);
      }
    }

    private void InitAlivePlayerCount() {
      int redCount = 0;
      int blueCount = 0;

      foreach (PhotonPlayer player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0)
          redCount += 1;
        else if (team == 1)
          blueCount += 1;
      }

      var props = new Hashtable() {{ "AlivePlayerCount", new int[] {redCount, blueCount} }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    private void ShowWin() {
      var result = Instantiate(_winObj);
      ResultProcess(result);
    }

    private void ShowLose() {
      var result = Instantiate(_loseObj);
      ResultProcess(result);
    }

    private void ShowDraw() {
      var result = Instantiate(_drawObj);
      ResultProcess(result);
    }

    private void ResultProcess(GameObject result) {
      _isFinished = true;

      result.transform.SetParent(_canvas.transform, false);

      MonoUtility.Instance.DelaySec(3.0f, () => {
        Destroy(result);
      });

      MonoUtility.Instance.DelaySec(10.0f, () => {
        PhotonNetwork.LeaveRoom();
        _camera.DisableTracking();
      });
    }

    [SerializeField] private GameObject _winObj;
    [SerializeField] private GameObject _loseObj;
    [SerializeField] private GameObject _drawObj;
    private bool _isFinished = false;
    private TimePanel _timePanel;
    private TrackCamera _camera;
    private Canvas _canvas;
  }
}

