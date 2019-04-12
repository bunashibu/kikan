using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
      _timestamp = Time.time;

      MonoUtility.Instance.StoppableDelaySec(_existTime, "NageSpaceFalse" + GetInstanceID().ToString(), () => {
        if (gameObject == null)
          return;

        gameObject.SetActive(false);

        // NOTE: See SMB-DestroySkillSelf
        MonoUtility.Instance.StoppableDelaySec(5.0f, "NageSpaceDestroy" + GetInstanceID().ToString(), () => {
          Destroy(gameObject);
        });
      });

      _collider = GetComponent<BoxCollider2D>();
      _collider.enabled = false;
      _animator = GetComponent<Animator>();
      _animator.enabled = false;
      _audioSource = GetComponent<AudioSource>();
      _audioSource.enabled = false;

      GetComponent<SpriteRenderer>().sprite = _prepareSprite;

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null) // May be not good
        .First(_ => Time.time - _timestamp >= _hideStartTime)
        .Subscribe(_ => {
          _player = _skillUserObj.GetComponent<Player>();
          _player.BodyCollider.enabled = false;
          _player.Renderers[0].enabled = false;
          _player.Renderers[1].enabled = false;

          MonoUtility.Instance.StoppableDelaySec(_hideTime, "NageSpaceHide" + GetInstanceID().ToString(), () => {
            _player.BodyCollider.enabled = true;
            _player.Renderers[0].enabled = true;
            _player.Renderers[1].enabled = true;
          });
        })
        .AddTo(this);

      this.UpdateAsObservable()
        .First(_ => Time.time - _timestamp >= _collisionOccurenceTime)
        .Subscribe(_ => {
          _collider.enabled = true;
          _animator.enabled = true;
          _audioSource.enabled = true;
        })
        .AddTo(this);
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Nage);
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(this);

      if (_player != null) {
        _player.BodyCollider.enabled = true;
        _player.Renderers[0].enabled = true;
        _player.Renderers[1].enabled = true;
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private Sprite _prepareSprite;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private BoxCollider2D _collider;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _timestamp;
    private float _collisionOccurenceTime = 0.5f;
    private float _existTime = 10.0f;

    private Player _player;
    private float _hideStartTime = 0.2f;
    private float _hideTime = 2.0f;
  }
}

