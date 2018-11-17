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
        .Subscribe(cur => enemy.WorldHpBar.UpdateView(cur, enemy.Hp.Max.Value))
        .AddTo(enemy.WorldHpBar);

      enemy.DebuffState.StateDictionary
        .ObserveReplace()
        .Subscribe(x => Debug.Log(x))
        .AddTo(enemy.gameObject);
    }
  }
}

