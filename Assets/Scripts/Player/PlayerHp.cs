using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHp : Hp {
  public PlayerHp(BattlePlayer player, DataTable hpTable, Bar worldBar) {
    _player = player;
    _hpTable = hpTable;
    _worldBar = worldBar;

    Min = 0;
    Max = _hpTable.Data[0];
    Cur = Max;

    if (_player.PhotonView.isMine)
      _worldBar.gameObject.SetActive(false);
  }

  public void AttachHudBar(Bar hudBar) {
    _hudBar = hudBar;
  }

  public void FullRecover() {
    Plus(Max);
  }

  public void UpdateMaxHp() {
    double ratio = (double)((_player.Core.Hp + 100) / 100.0);
    Max = (int)(_hpTable.Data[_player.Level.Lv - 1] * ratio);
  }

  public void UpdateView() {
    if (_player.PhotonView.isMine)
      _hudBar.UpdateView(Cur, Max);
    else
      _worldBar.UpdateView(Cur, Max);
  }

  protected override void Die() {
    base.Die();
    _player.SyncObserver.SyncIsDead();
  }

  /*                                                               *
   * INFO: ForceSyncXXX method must be called by SyncObserver only *
   *                                                               */
  public void ForceSyncHp(int cur, int min, int max) {
    Assert.IsTrue(_player.SyncObserver.ShouldSync);
    Cur = cur;
    Min = min;
    Max = max;
  }

  public void ForceSyncCur(int cur) {
    Assert.IsTrue(_player.SyncObserver.ShouldSync);
    Cur = cur;
  }

  public void ForceSyncMax(int max) {
    Assert.IsTrue(_player.SyncObserver.ShouldSync);
    Max = max;
  }

  public void ForceSyncIsDead(bool isDead) {
    Assert.IsTrue(_player.SyncObserver.ShouldSync);
    IsDead = isDead;
  }

  private BattlePlayer _player;
  private DataTable _hpTable;
  private Bar _hudBar;
  private Bar _worldBar;
}

