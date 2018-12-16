using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(AttackSynchronizer))]
  [RequireComponent(typeof(DebuffSynchronizer))]
  public class ManjiZ : Skill {
    void Awake() {
      _attackSynchronizer = GetComponent<AttackSynchronizer>();
      _debuffSynchronizer = GetComponent<DebuffSynchronizer>();
      _targetChecker = new TargetChecker(_targetNum);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider, _skillUserObj)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = collider.gameObject.GetComponent<IPhoton>();
        Assert.IsNotNull(target);

        _attackSynchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Manji);
        _debuffSynchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Stun, _stunSec);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;
    [SerializeField] private float _stunSec;

    private AttackSynchronizer _attackSynchronizer;
    private DebuffSynchronizer _debuffSynchronizer;
    private TargetChecker _targetChecker;
  }
}

