using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IDamageReactorSlot {
    int ReactTo(IOnAttacked target, IAttacker attacker, int damage, bool isCritical);
  }
}
