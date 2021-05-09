using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IDamageReactorSlot {
    int Calculate(IOnAttacked target, IAttacker attacker, int damage, bool isCritical);
  }
}
