using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerLevel : Level {
  public void Init(LevelPanel lvPanel, KillDeathPanel kdPanel) {
    Assert.IsTrue(photonView.isMine);

    int initialLv = 1;
    Init(initialLv);

    _lvPanel = lvPanel;
    _kdPanel = kdPanel;
  }

  public void UpdateView() {
    Assert.IsTrue(photonView.isMine);

    _lvPanel.UpdateView(Lv);
    _kdPanel.UpdateLvView(Lv);
  }

  public override void LvUp() {
    base.LvUp();

    if (photonView.isMine) {
      UpdateView();
      _playerHp.UpdateMaxHp();
      _playerHp.UpdateView();
    }
  }

  [SerializeField] private PlayerHp _playerHp;
  private LevelPanel _lvPanel;
  private KillDeathPanel _kdPanel;
}

