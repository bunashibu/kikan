using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class WarriorZ : Skill {
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
        .Subscribe(_ => {
          var player = _skillUserObj.GetComponent<Player>();
          transform.position = player.transform.position + player.Weapon.AppearOffset[2];
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Warrior);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _existTime = 7.0f;
  }
}
