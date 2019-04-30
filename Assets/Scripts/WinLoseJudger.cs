using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class WinLoseJudger : SingletonMonoBehaviour<WinLoseJudger> {
    void Start() {
      FindCanvas();

      if (PhotonNetwork.isMasterClient) {
        _photonView = GetComponent<PhotonView>();

        InitAlivePlayerCount();

        this.UpdateAsObservable()
          .Where(_ => _timePanel.TimeSec > 0)
          .Where(_ => _alivePlayerCount.Red == 0 && _alivePlayerCount.Blue > 0)
          .Take(1)
          .Subscribe(_ => SyncWinProcess(1) );

        this.UpdateAsObservable()
          .Where(_ => _timePanel.TimeSec > 0)
          .Where(_ => _alivePlayerCount.Blue == 0 && _alivePlayerCount.Red > 0)
          .Take(1)
          .Subscribe(_ => SyncWinProcess(0) );

        this.UpdateAsObservable()
          .Where(_ => _timePanel.TimeSec > 0)
          .Where(_ => _alivePlayerCount.Blue == 0 && _alivePlayerCount.Red == 0)
          .Take(1)
          .Subscribe(_ => SyncDraw() );

        this.UpdateAsObservable()
          .Where(_ => _timePanel.TimeSec <= 0)
          .Where(_ => _alivePlayerCount.Red > 0)
          .Where(_ => _alivePlayerCount.Blue > 0)
          .Take(1)
          .Subscribe(_ => SyncDraw() );
      }
    }

    private void FindCanvas() {
      _canvas = GameObject.Find("Canvas");
      if (_canvas == null)
        Debug.Log("Can't find Canvas");

      var timePanelObj = GameObject.Find("Canvas/TimePanel");
      if (timePanelObj == null)
        Debug.Log("Can't find TimePanelObj");
      else
        _timePanel = timePanelObj.GetComponent<TimePanel>();
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
      });
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

    private PhotonView _photonView;
    private TimePanel  _timePanel;
    private GameObject _canvas;

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

