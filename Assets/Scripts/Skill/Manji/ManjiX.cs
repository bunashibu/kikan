using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class ManjiX : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _targetChecker = new TargetChecker(_targetNum);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider, _skillUserObj)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = collider.gameObject.GetComponent<IPhoton>();
        Assert.IsNotNull(target);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Manji);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;

    private SkillSynchronizer _synchronizer;
    private TargetChecker _targetChecker;
  }
}

