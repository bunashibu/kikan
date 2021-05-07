using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Reduce : IDamageReactorSlot {
    public Reduce(float reduceRatio) {
      _reduceRatio = reduceRatio;
    }

    public int ReactTo(IOnAttacked target, IAttacker attacker, int damage) {
      int reducedDamage = (int)(damage * (1.0f - _reduceRatio));
      target.Hp.Subtract(reducedDamage);
      return reducedDamage;
    }

    private float _reduceRatio = 0;
  }
}
