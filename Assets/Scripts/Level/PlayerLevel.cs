using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : Level {
  public void Init(LevelPanel lvPanel, KillDeathPanel kdPanel) {
    _lvPanel = lvPanel;
    _kdPanel = kdPanel;
  }

  public void Show() {
    _lvPanel.Show(Lv);
    _kdPanel.UpdateLv(Lv);
  }

  public override void LvUp() {
    base.LvUp();
    Show();
  }

  private LevelPanel _lvPanel;
  private KillDeathPanel _kdPanel;
}

