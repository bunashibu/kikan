using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
      _timestamp = Time.time;

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Where(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          return Client.Opponents.Contains(skillUser);
        })
        .Subscribe(_ => _renderer.color = new Color(0, 1, 1, 1));

      Observable.Timer(TimeSpan.FromSeconds(_existTime))
        .Where(_ => this != null)
        .Subscribe(_ => {
          gameObject.SetActive(false);

          Observable.Timer(TimeSpan.FromSeconds(5.0f))
            .Where(none => this != null)
            .Subscribe(none => {
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
        .Where(_ => _skillUserObj != null)
        .Where(_ => Time.time - _timestamp >= _hideStartTime)
        .Take(1)
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
        });

      this.UpdateAsObservable()
        .Where(_ => Time.time - _timestamp >= _collisionOccurenceTime)
        .Take(1)
        .Subscribe(_ => {
          _collider.enabled = true;
          _animator.enabled = true;
          _audioSource.enabled = true;
        });

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Where(_ => _player.Level.Cur.Value >= 11)
        .Take(1)
        .Subscribe(_ => _attackInfo = new AttackInfo(150, _attackInfo.MaxDeviation, _attackInfo.CriticalPercent));
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
        SkillReference.Instance.Remove(viewID);

      if (_player != null) {
        _player.BodyCollider.enabled = true;
        _player.Renderers[0].enabled = true;
        _player.Renderers[1].enabled = true;
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private Sprite _prepareSprite;
    [SerializeField] private SpriteRenderer _renderer;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private BoxCollider2D _collider;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _timestamp;
    private float _collisionOccurenceTime = 0.45f;
    private float _existTime = 15.0f;

    private Player _player;
    private float _hideStartTime = 0.1f;
    private float _hideTime = 2.0f;
  }
}
