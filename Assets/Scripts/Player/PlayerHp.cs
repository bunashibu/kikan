using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : Hp {
  public PlayerHp(BattlePlayer player) {
    _player = player;
    _player.SyncObserver.SyncAllHp();
  }

  public void Init(Bar hudBar) {
    /*
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncHpAll", PhotonTargets.All, _hpTable.Data[0], 0, _hpTable.Data[0]);

    _hudBar = hudBar;
    _worldBar.gameObject.SetActive(false);
    */
  }

  public void UpdateView() {
    //photonView.RPC("SyncHpUpdate", PhotonTargets.All);
  }

  public override void Plus(int quantity) {
    base.Plus(quantity);
    _player.SyncObserver.SyncCurHp();
  }

  public override void Minus(int quantity) {
    base.Minus(quantity);
    _player.SyncObserver.SyncCurHp();
  }

  public void UpdateMaxHp() {
    /*
    double ratio = (double)((_player.Core.Hp + 100) / 100.0);
    Max = (int)(_hpTable.Data[_player.Level.Lv - 1] * ratio);

    _player.SyncObserver.SyncMaxHp();
    */
  }

  public void FullRecover() {
    Plus(Max);
  }

  private BattlePlayer _player;
}

