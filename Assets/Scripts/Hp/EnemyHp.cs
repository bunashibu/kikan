using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyHp : Hp {
    public EnemyHp() {
      /*
      _player = player;
      _hpTable = hpTable;
      _worldBar = worldBar;

      Max = _hpTable.Data[0];
      Cur = Max;

      if (_player.PhotonView.isMine)
        _worldBar.gameObject.SetActive(false);
      */
    }

    public override void Add(int quantity) {
      //base.Add(quantity);
      //_enemy.Observer.SyncCurHp();
    }

    public override void Subtract(int quantity) {
      //base.Subtract(quantity);
      //_enemy.Observer.SyncCurHp();
    }

    public override void UpdateView() {
      /*
      if (_enemy.PhotonView.isMine)
        _hudBar.UpdateView(Cur, Max);
      else
        _worldBar.UpdateView(Cur, Max);

      _player.Observer.SyncUpdateHpView();
      */
    }
  }
}

