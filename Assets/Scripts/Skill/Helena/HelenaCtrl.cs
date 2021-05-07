using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class HelenaCtrl : Skill {
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
    }

    void Update() {
      if (photonView.isMine)
        transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      var target = collider.gameObject.GetComponent<IPhoton>();

      if (target == null)
        return;
      if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
        return;
      if (_hitRistrictor.ShouldRistrict(collider.gameObject))
        return;

      var marginTime = 0.1f;
      Observable.Timer(TimeSpan.FromSeconds(marginTime))
        .Where(_ => this != null)
        .Subscribe(_ => {
          gameObject.SetActive(false);
        });

      if (PhotonNetwork.isMasterClient) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Helena);
        _synchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Ice, _duration);

      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _duration = 2.0f;
    private float _existTime = 0.6f;
    private float _moveSpeed = 8.0f;
  }
}
