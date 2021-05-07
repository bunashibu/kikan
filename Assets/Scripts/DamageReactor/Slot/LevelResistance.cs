using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LevelResistance : IDamageReactorSlot {
    public LevelResistance(int level) {
      _level = level;
    }

    public int ReactTo(IOnAttacked target, IAttacker attacker, int damage, bool isCritical) {
      float applyDamage = 1;

      if (_level <= attacker.CurLevel)
        applyDamage = damage;
      else if (_level - 1 == attacker.CurLevel)
        applyDamage = damage * 0.75f;
      else if (_level - 2 == attacker.CurLevel)
        applyDamage = damage * 0.5f;
      else if (_level - 3 == attacker.CurLevel)
        applyDamage = damage * 0.25f;

      target.Hp.Subtract((int)applyDamage);
      return (int)applyDamage;
    }

    private int _level;
  }
}
