using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : Level {
  public void Init(LevelPanel panel) {
    _panel = panel;
  }

  public void Show() {
    _panel.Show(Lv);
  }

  public override void LvUp() {
    base.LvUp();
    Show();
  }

  private LevelPanel _panel;
}

