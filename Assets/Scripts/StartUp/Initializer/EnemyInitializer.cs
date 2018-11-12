using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class EnemyInitializer : SingletonMonoBehaviour<EnemyInitializer> {
    public void Initialize(Enemy enemy) {
      enemy.Hp.Cur
        .SkipLatestValueOnSubscribe()
        .Subscribe(cur => enemy.WorldHpBar.UpdateView(cur, enemy.Hp.Max.Value))
        .AddTo(enemy.WorldHpBar);
    }
  }
}

