using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiX : Skill {
    void Awake() {
      _targetChecker = new TargetChecker(_targetNum);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider, _skillUserObj)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target   = collider.gameObject.GetComponent<IMediator>();
        var notifier = new Notifier(target.Mediator.OnNotify);

        notifier.Notify(Notification.TakeDamage, DamageCalculator.Damage, DamageCalculator.IsCritical, _skillUserObj);
      }
    }

    [SerializeField] AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;

    private TargetChecker _targetChecker;
  }
}

