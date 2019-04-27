using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Passing : IDamageReactorSlot {
    public int ReactTo(IOnAttacked target, int damage) {
      target.Hp.Subtract(damage);
      return damage;
    }
  }
}

