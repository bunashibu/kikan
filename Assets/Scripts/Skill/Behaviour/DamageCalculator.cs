using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageCalculator {
    public int CalculateDamage(int power, int maxDeviation, int criticalProbability) {
      int deviation = (int)((Random.value - 0.5) * 2 * maxDeviation);
      Damage = power + deviation;

      IsCritical = JudgeCritical(criticalProbability);
      if (IsCritical)
        Damage *= 2;

      return Damage;
    }

    private bool JudgeCritical(int probability) {
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        return true;

      return false;
    }

    public int Damage { get; private set; }
    public bool IsCritical { get; private set; }
  }
}

