using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Shield : IDamageReactorSlot {
    public Shield(int durability) {
      _durability = durability;
    }

    public int ReactTo(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      if (IsBroken) {
        target.Hp.Subtract(damage);
        return damage;
      }

      _durability -= (damage - 1);

      if (IsBroken) {
        target.Hp.Subtract(-_durability);
        return -_durability;
      }
      else {
        target.Hp.Subtract(1);
        return 1;
      }
    }

    public bool IsBroken => _durability <= 0;

    private int _durability;
  }
}
