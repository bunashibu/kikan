using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerLevel : Level {
  public void Init(LevelPanel lvPanel, KillDeathPanel kdPanel) {
    Assert.IsTrue(photonView.isMine);

    Init();
    _lvPanel = lvPanel;
    _kdPanel = kdPanel;
  }

  public void Show() {
    Assert.IsTrue(photonView.isMine);

    _lvPanel.Show(Lv);
    _kdPanel.UpdateLv(Lv);
  }

  public override void LvUp() {
    base.LvUp();

    if (photonView.isMine)
      Show();
  }

  private LevelPanel _lvPanel;
  private KillDeathPanel _kdPanel;
}

