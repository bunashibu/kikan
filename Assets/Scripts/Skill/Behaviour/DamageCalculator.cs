using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageCalculator : IObserver {
    public DamageCalculator(int increasePercent, int maxDeviation) {
      _increasePercent = increasePercent;
      _maxDeviation    = maxDeviation;
    }

    public void OnNotify(Notification notification, object[] args) {
      if (notification == Notification.HitSkill) {
        var target = ((Collider2D)args[0]).gameObject.GetComponent<IBattle>();

        CalculateCritical(target.Critical);
        CalculateDamage(target.Power);

        targetNotifier.Notify(Notification.TakeDamage, _isCritical, _damage);
      }
    }

    private void CalculateCritical(int probability) {
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        _isCritical = true;
      else
        _isCritical = false;
    }

    private void CalculateDamage(int basePower) {
      int deviation = (int)((Random.value - 0.5) * 2 * _maxDeviation);

      _damage = (int)((basePower * _increasePercent / 100.0) + deviation);

      if (_isCritical)
        _damage *= 2;
    }

    private int _increasePercent;
    private int _maxDeviation;

    private int _isCritical;
    private int _damage;
  }
}

