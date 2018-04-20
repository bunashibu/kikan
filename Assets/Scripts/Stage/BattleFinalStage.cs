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
        GatherPlayer();

        if (transform.rotation == _destRotation) {
          _isRotating = false;
          _timePanel.SetTime(_time);
          _camera.EnableTracking();
          ResetPlayerStatus();

          var judger = Instantiate(_judger).GetComponent<WinLoseJudger>() as WinLoseJudger;
          judger.SetTimePanel(_timePanel);
        }
      }
    }

    public void Emerge() {
      gameObject.SetActive(true);

      int viewID = (int)PhotonNetwork.player.CustomProperties["ViewID"];
      _player = PhotonView.Find(viewID).GetComponent<BattlePlayer>() as BattlePlayer;

      _player.Rigid.velocity = Vector3.zero;
      _player.Rigid.simulated = false;
      _player.Observer.SyncRigidSimulated();

      _player.Character.enabled = false;
      _player.Weapon.SkillInstantiator.enabled = false;

      _camera.DisableTracking();
      _camera.DisableRestrict();

      SkillReference.Instance.DeleteAll();
      MonoUtility.Instance.StopAll();

      PrepareEasing();
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    public void StartRotation() {
      _isRotating = true;
    }

    private void PrepareEasing() {
      Vector3 startPosition = _player.Transform.position;
      Vector3 startCameraPosition = _camera.transform.position;
      Vector3 gatherPosition = StageReference.Instance.StageData.RespawnPosition;
      if (_player.PlayerInfo.Team == 1)
        gatherPosition.x *= -1;

      _easing = new QuadraticEaseInOut(startPosition, gatherPosition, 3.0f);
      _easingCamera = new QuadraticEaseInOut(startCameraPosition, gatherPosition + new Vector3(0, 0, _camera.OffsetZ), 3.0f);
    }

    private void Rotate() {
      float step = _rotateSpeed * Time.deltaTime;
      transform.rotation = Quaternion.RotateTowards(transform.rotation, _destRotation, step);
    }

    private void GatherPlayer() {
      _player.Transform.position = _easing.GetNextPosition();
      _camera.transform.position = _easingCamera.GetNextPosition();
    }

    private void ResetPlayerStatus() {
      _player.Rigid.simulated = true;
      _player.Observer.SyncRigidSimulated();

      _player.Character.enabled = true;
      _player.Weapon.SkillInstantiator.enabled = true;

      _player.StateTransfer.TransitTo("Idle", _player.Animator);

      _player.Hp.FullRecover();
      _player.Hp.UpdateView();

      _player.BuffState.Reset();

      _player.Weapon.SkillInstantiator.ResetAllCT();
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

    private Quaternion _destRotation;
    private bool _isRotating;
    private BattlePlayer _player;
    private QuadraticEaseInOut _easing;
    private QuadraticEaseInOut _easingCamera;
  }
}

