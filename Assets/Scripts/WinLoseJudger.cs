using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class WinLoseJudger : SingletonMonoBehaviour<WinLoseJudger> {
    void Start() {
      if (PhotonNetwork.isMasterClient) {
        _photonView = GetComponent<PhotonView>();

        InitAlivePlayerCount();

        MonoUtility.Instance.DelayUntil(() => _timePanel.TimeSec <= 0, () => {
          if ( (_alivePlayerCount.Red > 0) && (_alivePlayerCount.Blue > 0) )
            SyncDraw();
        });
      }
    }

    void Update() {
      if (PhotonNetwork.isMasterClient) {
        if (_isFinished)
          return;

        if (_alivePlayerCount.Red == 0)
          SyncWinProcess(1);

        if (_alivePlayerCount.Blue == 0)
          SyncWinProcess(0);
      }
    }

    [PunRPC]
    private void SyncDrawRPC() {
      ShowDraw();
    }

    private void SyncDraw() {
      _photonView.RPC("SyncDrawRPC", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    private void SyncWinProcessRPC(int winTeam) {
      int team = (int)PhotonNetwork.player.CustomProperties["Team"];

      if (team == winTeam)
        ShowWin();
      else
        ShowLose();

      _isFinished = true;
    }

    private void SyncWinProcess(int winTeam) {
      _photonView.RPC("SyncWinProcessRPC", PhotonTargets.AllViaServer, winTeam);
    }

    public void UpdateAlivePlayerCount(Player player) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      if (player.PlayerInfo.Team == 0)
        _alivePlayerCount.Red -= 1;

      if (player.PlayerInfo.Team == 1)
        _alivePlayerCount.Blue -= 1;
    }

    private void InitAlivePlayerCount() {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      int redCount = 0;
      int blueCount = 0;

      foreach (PhotonPlayer player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0)
          redCount += 1;
        else if (team == 1)
          blueCount += 1;
      }

      _alivePlayerCount = new AlivePlayerCount(redCount, blueCount);
    }

    private void ResultProcess(GameObject result) {
      Instantiate(_transporter);

      result.transform.SetParent(_canvas.transform, false);

      MonoUtility.Instance.DelaySec(4.0f, () => {
        Destroy(result);
      });

      MonoUtility.Instance.DelaySec(10.0f, () => {
        SkillReference.Instance.DeleteAll();
        MonoUtility.Instance.StopAll();
        SceneChanger.Instance.FadeOutAndLeaveRoom();
        _camera.DisableTracking();
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

    [SerializeField] private GameObject _winObj;
    [SerializeField] private GameObject _loseObj;
    [SerializeField] private GameObject _drawObj;
    [SerializeField] private AfterFinalBatlleTransporter _transporter;

    private bool _isFinished = false;

    private PhotonView  _photonView;
    private TimePanel   _timePanel;
    private TrackCamera _camera;
    private Canvas      _canvas;

    private AlivePlayerCount _alivePlayerCount;
  }

  public class AlivePlayerCount {
    public AlivePlayerCount(int red, int blue) {
      Red = red;
      Blue = blue;
    }

    public int Red;
    public int Blue;
  }
}

