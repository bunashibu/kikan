using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PowerCalculator {
    public int CalculatePlayerPower(BattlePlayer player) {
      double ratio = (double)((player.Core.Attack + 100) / 100.0);

      return (int)(player.Status.Atk * ratio);
    }
  }
}

