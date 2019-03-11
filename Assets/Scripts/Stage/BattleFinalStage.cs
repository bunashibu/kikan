using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleFinalStage : MonoBehaviour {
    void Awake() {
      _destRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update() {
      if (_isRotating) {
        Rotate();

        if (transform.rotation == _destRotation)
          _isRotating = false;
      }

      if (_isNotReady) {
        if (Time.time - _timestamp < _prepareDuration)
          GatherPlayer();
        else {
          SetTimeAndCamera();
          ResetPlayerStatus();
          InstantiateWinLoseJudger();
          _isNotReady = false;
        }
      }
    }

    public void Emerge() {
      gameObject.SetActive(true);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    public void StartRotation() {
      _isRotating = true;
      _timestamp = Time.time;
    }

    public void Prepare() {
      PreparePlayerToGather();
      PrepareEasing();

      SkillReference.Instance.DeleteAll();
      MonoUtility.Instance.StopAll();
    }

    private void PreparePlayerToGather() {
      foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList) {
        int viewID = (int)photonPlayer.CustomProperties["ViewID"];
        var player = PhotonView.Find(viewID).GetComponent<Player>() as Player;

        if (photonPlayer == PhotonNetwork.player)
          _player = player;

        player.Rigid.velocity = Vector3.zero;
        player.Rigid.simulated = false;

        player.Character.enabled = false;
        player.Weapon.SkillInstantiator.enabled = false;
      }
    }

    private void PrepareEasing() {
      _camera.DisableTracking();
      _camera.DisableRestrict();

      Vector3 startPosition = _player.transform.position;
      Vector3 startCameraPosition = _camera.transform.position;
      Vector3 gatherPosition = StageReference.Instance.StageData.RespawnPosition;
      if (_player.PlayerInfo.Team == 1)
        gatherPosition.x *= -1;

      _easing = new QuadraticEaseInOut(startPosition, gatherPosition, _prepareDuration);
      _easingCamera = new QuadraticEaseInOut(startCameraPosition, gatherPosition + new Vector3(0, 0, _camera.OffsetZ), _prepareDuration);
    }

    private void Rotate() {
      float step = _rotateSpeed * Time.deltaTime;
      transform.rotation = Quaternion.RotateTowards(transform.rotation, _destRotation, step);
    }

    private void GatherPlayer() {
      _player.transform.position = _easing.GetNextPosition();
      _camera.transform.position = _easingCamera.GetNextPosition();
    }

    private void SetTimeAndCamera() {
      _timePanel.SetTime(_time);
      _camera.EnableTracking();
    }

    private void ResetPlayerStatus() {
      foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList) {
        int viewID = (int)photonPlayer.CustomProperties["ViewID"];
        var player = PhotonView.Find(viewID).GetComponent<Player>() as Player;

        player.Rigid.simulated = true;

        player.BodyCollider.enabled = true;

        player.Character.enabled = true;
        player.Weapon.SkillInstantiator.enabled = true;

        player.StateTransfer.TransitTo("Idle", player.Animator);

        player.Hp.FullRecover();

        //_player.BuffState.Reset();
      }

      _player.Weapon.SkillInstantiator.ResetAllCT();

      MonoUtility.Instance.DelaySec(2.0f, () => {
        _player.PopupRemark.gameObject.SetActive(false);
      });
    }

    private void InstantiateWinLoseJudger() {
      var judger = Instantiate(_judger).GetComponent<WinLoseJudger>() as WinLoseJudger;
      judger.SetTimePanel(_timePanel);
      judger.SetCamera(_camera);
      judger.SetCanvas(_canvas);
    }

    [SerializeField] private TimePanel _timePanel;

    [Header("Final Battle Time (Second)")]
    [SerializeField] private float _time;
    [Header("Rotation per second")]
    [SerializeField] private float _rotateSpeed;

    [Space(10)]
    [SerializeField] private WinLoseJudger _judger;
    [Space(10)]
    [SerializeField] private TrackCamera _camera;
    [Space(10)]
    [SerializeField] private Canvas _canvas;

    private Quaternion _destRotation;
    private bool _isRotating;
    private bool _isNotReady = true;
    private float _timestamp;
    private float _prepareDuration = 5.0f;
    private Player _player;
    private QuadraticEaseInOut _easing;
    private QuadraticEaseInOut _easingCamera;
  }
}

