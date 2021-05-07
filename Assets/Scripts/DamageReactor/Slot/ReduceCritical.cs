using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ReduceCritical : IDamageReactorSlot {
    public ReduceCritical(float applyRatio) {
      _applyRatio = applyRatio;
    }

    public int ReactTo(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      if (isCritical) {
        int reducedDamage = (int)(damage / 2 * _applyRatio);
        target.Hp.Subtract(reducedDamage);
        return reducedDamage;
      }

      target.Hp.Subtract(damage);
      return damage;
    }

    private float _applyRatio;
  }
}
