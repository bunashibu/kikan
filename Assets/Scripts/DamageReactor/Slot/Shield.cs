using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Shield : IDamageReactorSlot {
    public Shield(int durability) {
      _durability = durability;
    }

    public int Calculate(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      if (IsBroken)
        return damage;

      _durability -= (damage - 1);

      if (IsBroken)
        return -_durability;
      else
        return 1;
    }

    public bool IsBroken => _durability <= 0;

    private int _durability;
  }
}
