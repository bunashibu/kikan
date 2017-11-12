using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerHp : Hp {
    public PlayerHp(BattlePlayer player, DataTable hpTable, Bar worldBar) {
      _player = player;
      _hpTable = hpTable;
      _worldBar = worldBar;

      Max = _hpTable.Data[0];
      Cur = Max;

      if (_player.PhotonView.isMine)
        _worldBar.gameObject.SetActive(false);
    }

    public override void Add(int quantity) {
      base.Add(quantity);
      _player.Observer.SyncCurHp();
    }

    public override void Subtract(int quantity) {
      base.Subtract(quantity);
      _player.Observer.SyncCurHp();
    }

    public void UpdateView() {
      _player.Observer.SyncUpdateHpView();
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

    /*                                                            *
     * INFO: ForceSyncXXX method must be called ONLY by Observer. *
     *       Otherwise it breaks encapsulation.                   *
     *                                                            */
    public void ForceSync(int cur, int max) {
      Assert.IsTrue(_player.Observer.ShouldSync("Hp"));
      Cur = cur;
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

    public void ForceSyncUpdateView() {
      Assert.IsTrue(_player.Observer.ShouldSync("UpdateHpView"));

      if (_player.PhotonView.isMine)
        _hudBar.UpdateView(Cur, Max);
      else
        _worldBar.UpdateView(Cur, Max);
    }

    private BattlePlayer _player;
    private DataTable _hpTable;
    private Bar _hudBar;
    private Bar _worldBar;
  }
}

