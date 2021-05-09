using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageReactor {
    public DamageReactor(IOnAttacked target) {
      _target = target;
      Slot = new Passing();
      SubSlot = new Passing();
    }

    public void SetSlot(IDamageReactorSlot slot) {
      Slot = slot;
    }

    public void SetSubSlot(IDamageReactorSlot slot) {
      SubSlot = slot;
    }

    public int ReactTo(IAttacker attacker, int rawDamage, bool isCritical) {
      int damage = rawDamage;
      damage = Slot.Calculate(_target, attacker, damage, isCritical);
      damage = SubSlot.Calculate(_target, attacker, damage, isCritical);

      _target.Hp.Subtract(damage);
      return damage;
    }

    public IDamageReactorSlot Slot;
    public IDamageReactorSlot SubSlot;

    private IOnAttacked _target;
  }
}
