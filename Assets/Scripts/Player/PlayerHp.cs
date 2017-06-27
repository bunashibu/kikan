using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : Hp {
  public PlayerHp(BattlePlayer player) {
    _player = player;
  }

  public void UpdateMaxHp() {
    double ratio = (double)((_player.Core.Hp + 100) / 100.0);
    Max = (int)(_hpTable.Data[_player.Level.Lv - 1] * ratio);

    // TODO: Notify the Observer of MaxHp
  }

  public void FullRecover() {
    Plus(Max);
  }

  private BattlePlayer _player;
}

