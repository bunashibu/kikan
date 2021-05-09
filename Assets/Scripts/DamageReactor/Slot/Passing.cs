using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Passing : IDamageReactorSlot {
    public int Calculate(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      return damage;
    }
  }
}
