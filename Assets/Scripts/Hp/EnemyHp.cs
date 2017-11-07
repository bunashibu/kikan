using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyHp : Hp {
    public EnemyHp(Enemy enemy, PlainBar bar, int maxHp) {
      _enemy = enemy;
      _bar = bar;

      Max = maxHp;
      Cur = Max;

      _bar.gameObject.SetActive(false);
    }

    public override void Add(int quantity) {
      base.Add(quantity);
      //_enemy.Observer.SyncCurHp();
    }

    public override void Subtract(int quantity) {
      base.Subtract(quantity);
      //_enemy.Observer.SyncCurHp();
    }

    public override void UpdateView() {
      _bar.gameObject.SetActive(true);
      _bar.UpdateView(Cur, Max);

      //_bar.gameObject.SetActive(false);
    }

    private Enemy _enemy;
    private PlainBar _bar;
  }
}

