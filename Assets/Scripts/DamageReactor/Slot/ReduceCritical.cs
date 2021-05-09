using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ReduceCritical : IDamageReactorSlot {
    public ReduceCritical(float applyRatio) {
      _applyRatio = applyRatio;
    }

    public int Calculate(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      if (isCritical) {
        int reducedDamage = (int)(damage / 2 * _applyRatio);
        return reducedDamage;
      }

      return damage;
    }

    private float _applyRatio;
  }
}
