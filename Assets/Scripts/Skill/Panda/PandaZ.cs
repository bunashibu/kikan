using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class PandaZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      MonoUtility.Instance.StoppableDelaySec(_existTime, "PandaZFalse" + GetInstanceID().ToString(), () => {
        gameObject.SetActive(false);

        // NOTE: See SMB-DestroySkillSelf
        MonoUtility.Instance.StoppableDelaySec(5.0f, "PandaZDestroy" + GetInstanceID().ToString(), () => {
          Destroy(gameObject);
        });
      });

      this.UpdateAsObservable()
        .Where(_ => photonView.isMine)
        .First(_ => _skillUserObj != null)
        .Subscribe(_ => {
          _player = _skillUserObj.GetComponent<Player>();
        })
        .AddTo(this);

      this.UpdateAsObservable()
        .Where(_ => photonView.isMine)
        .Where(_ => _player != null)
        .Subscribe(_ => {
          UpdateMovement();
        })
        .AddTo(this);
    }

    void Start() {
      transform.parent = _skillUserObj.transform;
    }

    void OnTriggerStay2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Panda);
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(this);
    }

    private void UpdateMovement() {
      if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
        GroundMove();
        return;
      }

      if (_player.Location.IsGround && Input.GetButton("Jump")) {
        GroundJump();
        return;
      }
    }

    private void GroundMove() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

      if (OnlyLeftKeyDown)  {
        float angle;

        if (_player.Location.IsLeftSlope)
          angle = _player.Location.SlopeAngle;
        else if (_player.Location.IsRightSlope)
          angle = _player.Location.GroundAngle * -1;
        else if (_player.Location.IsRightGround)
          angle = _player.Location.GroundAngle * -1;
        else
          angle = _player.Location.GroundAngle;

        _player.Movement.GroundMoveLeft(angle);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      if (OnlyRightKeyDown) {
        float angle;

        if (_player.Location.IsRightSlope)
          angle = _player.Location.SlopeAngle;
        else if (_player.Location.IsLeftSlope)
          angle = _player.Location.GroundAngle * -1;
        else if (_player.Location.IsLeftGround)
          angle = _player.Location.GroundAngle * -1;
        else
          angle = _player.Location.GroundAngle;

        _player.Movement.GroundMoveRight(angle);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private void GroundJump() {
      _isJumping = true;
      _player.Movement.GroundJump();
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private Player _player;
    // NOTE:  _existTime must be the same value with Panda-Fist-Weapon-RigorCT because
    //        PandaZ manages input forcely. Otherwise, input duplication will occur.
    //        (By default, all input is managed in SMB)
    private float _existTime = 4.0f;
    private bool _isJumping;
  }
}

