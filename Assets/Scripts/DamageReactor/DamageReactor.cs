using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageReactor {
    public DamageReactor(IOnAttacked target) {
      _target = target;
      _slot = new Passing();
    }

    public void SetSlot(IDamageReactorSlot slot) {
      _slot = slot;
    }

    public int ReactTo(int damage) {
      return _slot.ReactTo(_target, damage);
    }

    private IOnAttacked _target;
    private IDamageReactorSlot _slot;
  }
}

