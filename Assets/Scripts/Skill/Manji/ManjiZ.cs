using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class ManjiZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = collider.gameObject.GetComponent<IPhoton>();
        Assert.IsNotNull(target);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Manji);
        _synchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Stun, _stunSec);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;
    [SerializeField] private float _stunSec;

    private SkillSynchronizer _synchronizer;
  }
}

