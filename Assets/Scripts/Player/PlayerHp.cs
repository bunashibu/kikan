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
    Add(Max);
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

  /*                                                            *
   * INFO: ForceSyncXXX method must be called ONLY by Observer. *
   *       Otherwise it breaks encapsulation.                   *
   *                                                            */
  public void ForceSync(int cur, int min, int max) {
    Assert.IsTrue(_player.Observer.ShouldSync("Hp"));
    Cur = cur;
    Min = min;
    Max = max;
  }

  public void ForceSyncCur(int cur) {
    Assert.IsTrue(_player.Observer.ShouldSync("CurHp"));
    Cur = cur;
  }

  public void ForceSyncMax(int max) {
    Assert.IsTrue(_player.Observer.ShouldSync("MaxHp"));
    Max = max;
  }

  private BattlePlayer _player;
  private DataTable _hpTable;
  private Bar _hudBar;
  private Bar _worldBar;
}

