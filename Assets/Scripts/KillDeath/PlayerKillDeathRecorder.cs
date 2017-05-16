using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillDeathRecorder : KillDeathRecorder {
  public void Init(KillDeathPanel kdPanel) {
    Init();
    _kdPanel = kdPanel;
  }

  public override void RecordKill() {
    base.RecordKill();
    _kdPanel.UpdateKill(KillCnt);
  }

  public override void RecordDeath() {
    base.RecordDeath();
    _kdPanel.UpdateDeath(DeathCnt);
  }

  private KillDeathPanel _kdPanel;
}

