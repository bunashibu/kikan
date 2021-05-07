﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public static class DamageCalculator {
    public static void Calculate(GameObject attackerObj, AttackInfo attackInfo, bool enabledCritical = true) {
      var attacker = attackerObj.GetComponent<IAttacker>();
      Assert.IsNotNull(attacker);

      if (enabledCritical)
        CalculateCritical(attacker.Critical + attackInfo.CriticalPercent);
      else
        IsCritical = false;

      CalculateDamage(attacker.Power, attackInfo.DamagePercent, attackInfo.MaxDeviation);
    }

    private static void CalculateCritical(int probability) {
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        IsCritical = true;
      else
        IsCritical = false;
    }

    private static void CalculateDamage(int basePower, int damagePercent, int maxDeviation) {
      int deviation = (int)((Random.value - 0.5) * 2 * maxDeviation);

      Damage = (int)((basePower * damagePercent / 100.0) + deviation);

      if (IsCritical)
        Damage *= 2;
    }

    public static int  Damage     { get; private set; }
    public static bool IsCritical { get; private set; }
  }
}
