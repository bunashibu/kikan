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
      if (_isFinished) {
        Debug.Log("XXXXXX OnConnectedToMaster() was called");
        PhotonNetwork.JoinRoom("Lobby");
      }
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      if (_isFinished) {
        Debug.Log("XXXXXX Failed to Join Room!");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)20;

        PhotonNetwork.JoinOrCreateRoom("Lobby", roomOptions, null);
      }
    }

    public override void OnJoinedRoom() {
      if (_isFinished) {
        Debug.Log("XXXXXX Joined Room!");
        SceneChanger.Instance.ChangeScene("Lobby");
      }
    }

    public override void OnLeftRoom() {
      if (_isFinished) {
        if (PhotonNetwork.connected)
          PhotonNetwork.JoinRoom("Lobby");
        else
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
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
      Debug.Log("Win");
      _isFinished = true;
      ReturnToLobby();
    }

    private void ShowLose() {
      Debug.Log("Lose");
      _isFinished = true;
      ReturnToLobby();
    }

    private void ShowDraw() {
      Debug.Log("Draw");
      _isFinished = true;
      ReturnToLobby();
    }

    private void ReturnToLobby() {
      MonoUtility.Instance.DelaySec(10.0f, () => {
        PhotonNetwork.LeaveRoom();
        _camera.DisableTracking();
      });
    }

    private bool _isFinished = false;
    private readonly string _gameVersion = "1.0b1";
    private TimePanel _timePanel;
    private TrackCamera _camera;
  }
}

