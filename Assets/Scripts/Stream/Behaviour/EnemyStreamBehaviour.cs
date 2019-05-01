using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class EnemyStreamBehaviour : SingletonMonoBehaviour<EnemyStreamBehaviour> {
    public void Initialize(Enemy enemy) {
      enemy.Hp.Cur
        .Where(cur => (cur <= 0))
        .Subscribe(_ => enemy.StateTransfer.TransitTo("Die"))
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

      enemy.FixSpd
        .ObserveCountChanged(true)
        .Where(count => count == 0)
        .Subscribe(_ => enemy.Movement.SetMoveForce(enemy.Spd) )
        .AddTo(enemy.gameObject);

      enemy.FixSpd
        .ObserveCountChanged(true)
        .Where(count => count > 0)
        .Subscribe(count => {
          bool isMostReacentBuff = true;

          // INFO: Set most recent debuff. If there are no debuff, most recent buff will be set.
          for (var j=count-1; j >= 0; --j) {
            if (enemy.FixSpd[j].Type == FixSpdType.Debuff) {
              enemy.Movement.SetMoveForce(enemy.FixSpd[j].Value);
              return;
            }

            if (isMostReacentBuff && enemy.FixSpd[j].Type == FixSpdType.Buff) {
              enemy.Movement.SetMoveForce(enemy.FixSpd[j].Value);
              isMostReacentBuff = false;
            }
          }
        })
        .AddTo(enemy.gameObject);
    }
  }
}

