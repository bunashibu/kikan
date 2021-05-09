using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Reduce : IDamageReactorSlot {
    public Reduce(float reduceRatio) {
      ReduceRatio = reduceRatio;
    }

    public int Calculate(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      int reducedDamage = (int)(damage * (1.0f - ReduceRatio));
      return reducedDamage;
    }

    public float ReduceRatio = 0;
  }
}
