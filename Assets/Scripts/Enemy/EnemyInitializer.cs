using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class EnemyInitializer : SingletonMonoBehaviour<EnemyInitializer> {
    public void Initialize(Enemy enemy) {
      enemy.Hp.Cur
        .Where(cur => (cur <= 0))
        .Subscribe(_ => enemy.StateTransfer.TransitTo("Die", enemy.Animator))
        .AddTo(enemy.gameObject);

      enemy.Hp.Cur
        .SkipLatestValueOnSubscribe()
        .Subscribe(_ => enemy.WorldHpBar.UpdateView(enemy.Hp.Cur.Value, enemy.Hp.Max.Value))
        .AddTo(enemy.WorldHpBar);

      enemy.Debuff.State
        .ObserveReplace()
        .Where(state => state.NewValue)
        .Subscribe(state => enemy.Debuff.Instantiate(state.Key))
        .AddTo(enemy.gameObject);

      enemy.Debuff.State
        .ObserveReplace()
        .Where(state => !state.NewValue)
        .Subscribe(state => enemy.Debuff.Destroy(state.Key))
        .AddTo(enemy.gameObject);
    }
  }
}

