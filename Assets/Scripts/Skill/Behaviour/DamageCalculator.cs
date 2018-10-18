using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class DamageCalculator : IListener {
    public void OnNotify(Notification notification, object[] args) {
      if (notification == Notification.HitSkill) {
        Assert.IsTrue(args.Length == 3);

        var attacker   = ((GameObject)args[0]).GetComponent<IAttacker>();
        var attackInfo = (AttackInfo)args[2];

        CalculateCritical(attacker.Critical + attackInfo.CriticalPercent);
        CalculateDamage(attacker.Power, attackInfo.DamagePercent, attackInfo.MaxDeviation);

        var taker = ((Collider2D)args[1]).gameObject.GetComponent<INotifier>();
        taker.Notifier.Notify(Notification.TakeDamage, _isCritical, _damage);
      }
    }

    private void CalculateCritical(int probability) {
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        _isCritical = true;
      else
        _isCritical = false;
    }

    private void CalculateDamage(int basePower, int damagePercent, int maxDeviation) {
      int deviation = (int)((Random.value - 0.5) * 2 * maxDeviation);

      _damage = (int)((basePower * damagePercent / 100.0) + deviation);

      if (_isCritical)
        _damage *= 2;
    }

    private bool _isCritical;
    private int  _damage;
  }
}

