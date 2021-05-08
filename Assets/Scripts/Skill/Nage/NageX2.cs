using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageX2 : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

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
    }

    void Start() {
      if (photonView.isMine)
        _moveDirection = transform.eulerAngles.y == 180 ? Vector2.right : Vector2.left;
    }

    void Update() {
      if (photonView.isMine)
        transform.Translate(_moveDirection * _spd * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        if (_isSecond) {
          _attackInfo = new AttackInfo(_attackInfo.DamagePercent, _attackInfo.MaxDeviation, 65);
          DamageCalculator.Calculate(_skillUserObj, _attackInfo);
          _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, (int)(DamageCalculator.Damage * _secondRatio), DamageCalculator.IsCritical, HitEffectType.Nage);
        }
        else {
          DamageCalculator.Calculate(_skillUserObj, _attackInfo);
          _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Nage);
          _isSecond = true;
        }
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(viewID);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private SpriteRenderer _renderer;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private Vector2 _moveDirection;
    private float _spd = 12.0f;
    private float _existTime = 0.37f;
    private bool _isSecond;
    private float _secondRatio = 0.125f;
  }
}
