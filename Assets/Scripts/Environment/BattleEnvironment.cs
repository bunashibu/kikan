using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public static class BattleEnvironment {
    public static Action<IBattle, int, bool> OnAttacked(IBattle target, Action<int, bool, IBattle, IBattle> PopupNumber) {
      Action<IBattle, int, bool> onAttacked = (attacker, damage, isCritical) => {
        target.Hp.Subtract(damage);
        PopupNumber(damage, isCritical, attacker, target);

        if (target.Hp.Cur.Value == target.Hp.Min.Value)
          target.OnKilled(attacker);
      };

      return onAttacked;
    }

    public static Action<IBattle> OnKilled(IBattle target, Func<IBattle, KillReward> GetRewardFrom, Action<IBattle, KillReward> GiveRewardTo) {
      Action<IBattle> onKilled = (attacker) => {
        if (attacker.Tag == "Player") {
          GiveRewardTo(attacker, GetRewardFrom(target));

          if (target.Tag == "Player") {
            /*
            attacker.KillCount.Value += 1
            target.DeathCount.Value += 1;
            */
          }
        }
      };

      return onKilled;
    }
  }
}

