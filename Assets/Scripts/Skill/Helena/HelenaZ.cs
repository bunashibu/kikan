using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class HelenaZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

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

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          Vector2 direction = (skillUser.Renderers[0].flipX) ? Vector2.left : Vector2.right;
          _synchronizer.SyncForce(skillUser.PhotonView.viewID, _force, direction, false);
        });

      Observable.Timer(TimeSpan.FromSeconds(_firstDamageTime))
        .Subscribe(_ => {
          _attackInfo = new AttackInfo(_continuousDamagePercent, _attackInfo.MaxDeviation, _attackInfo.CriticalPercent);
        });
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
        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Helena);
      }
    }


    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _existTime = 5.0f;
    private float _force = 15.0f;
    private int _continuousDamagePercent = 20;
    private float _firstDamageTime = 0.3f;
  }
}
