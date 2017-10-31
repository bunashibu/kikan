using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class KillDeathRecorder {
    public void RecordKillDeath(BattlePlayer target, BattlePlayer skillUser) {
      target.KillDeath.RecordDeath();
      target.KillDeath.UpdateDeathView();

      skillUser.KillDeath.RecordKill();
      skillUser.KillDeath.UpdateKillView();
    }
  }
}

