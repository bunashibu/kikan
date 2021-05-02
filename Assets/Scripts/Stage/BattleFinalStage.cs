using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class BattleFinalStage : MonoBehaviour {
    void Awake() {
      _destRotation = Quaternion.Euler(0, 0, 0);

      this.UpdateAsObservable()
        .Where(_ => _isRotating)
        .Subscribe(_ => Rotate() );

      this.UpdateAsObservable()
        .Where(_ => transform.rotation == _destRotation)
        .Take(1)
        .Subscribe(_ => _isRotating = false );

      this.UpdateAsObservable()
        .Where(_ => Time.time - _startPrepareTime < _prepareDuration)
        .Subscribe(_ => GatherPlayer() );

      this.UpdateAsObservable()
        .Where(_ => Time.time - _startPrepareTime >= _prepareDuration)
        .Take(1)
        .Subscribe(_ => {
          SetTimeAndCamera();
          ResetPlayerStatus();

          if (PhotonNetwork.isMasterClient)
            InstantiateWinLoseJudger();
        });
    }

    public void Emerge() {
      gameObject.SetActive(true);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    public void StartRotation() {
      _isRotating = true;
      _startPrepareTime = Time.time;
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

        player.Character.Disable();
        player.Weapon.DisableInstantiate();
        player.Weapon.ResetAllCT();
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

        player.Character.Enable();
        player.Weapon.EnableInstantiate();

        player.State.Rigor = false;

        player.StateTransfer.TransitTo("Idle");

        player.Debuff.DestroyAll();
        player.Debuff.Enable();

        player.Movement.SetMoveForce(player.Status.Spd);

        player.PopupRemark.gameObject.SetActive(false);

        player.Hp.FullRecover();
      }
    }

    private void InstantiateWinLoseJudger() {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var path = "Prefabs/WinLoseJudger";
      PhotonNetwork.Instantiate(path, Vector3.zero, Quaternion.identity, 0).GetComponent<WinLoseJudger>();
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
    private float _startPrepareTime;
    private float _prepareDuration = 4.0f;
    private Player _player;
    private QuadraticEaseInOut _easing;
    private QuadraticEaseInOut _easingCamera;
  }
}
