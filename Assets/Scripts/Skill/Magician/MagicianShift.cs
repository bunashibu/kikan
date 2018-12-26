using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianShift : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _targetChecker = new TargetChecker(_targetNum);
      _renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider, _skillUserObj)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = collider.gameObject.GetComponent<IPhoton>();
        Assert.IsNotNull(target);

        var direction = _renderer.flipX ? Vector2.right : Vector2.left;

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Magician);
        _synchronizer.SyncForce(target.PhotonView.viewID, _force, direction);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;
    [SerializeField] private float _force;

    private SkillSynchronizer _synchronizer;
    private TargetChecker _targetChecker;
    private SpriteRenderer _renderer;
  }
}

