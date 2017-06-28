using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : Hp {
  public PlayerHp(BattlePlayer player, DataTable hpTable) {
    _player = player;
    _hpTable = hpTable;

    Min = 0;
    Max = _hpTable.Data[0];
    Cur = Max;
  }

  public void UpdateMaxHp() {
    /*
    double ratio = (double)((_player.Core.Hp + 100) / 100.0);
    Max = (int)(_hpTable.Data[_player.Level.Lv - 1] * ratio);
    */

    _player.SyncObserver.SyncPlayerMaxHp();
  }

  public void FullRecover() {
    Plus(Max);
  }

  // Must be Called by SyncObserver only
  public void ForceSyncHp(int cur, int min, int max) {
    Cur = cur;
    Min = min;
    Max = max;
  }

  // Must be Called by SyncObserver only
  public void ForceSyncCur(int cur) {
    Cur = cur;
  }

  // Must be Called by SyncObserver only
  public void ForceSyncMax(int max) {
    Max = max;
  }

  // Must be Called by SyncObserver only
  public void ForceSyncIsDead(bool isDead) {
    IsDead = isDead;
  }

  private BattlePlayer _player;
  private DataTable _hpTable;
}

