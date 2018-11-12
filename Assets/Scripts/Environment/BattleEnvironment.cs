using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    public static Action<IBattle> OnKilled(IBattle target, Func<IBattle, int, KillReward> GetRewardFrom, Action<IPlayer, KillReward> GiveRewardTo) {
      Action<IBattle> onKilled = (attacker) => {
        if (attacker.Tag == "Player") {
          var killPlayer = attacker.gameObject.GetComponent<IPlayer>();
          Assert.IsNotNull(killPlayer);

          GiveRewardTo(killPlayer, GetRewardFrom(target, killPlayer.Teammates.Count));

          if (target.Tag == "Player") {
            var deathPlayer = target.gameObject.GetComponent<IPlayer>();
            Assert.IsNotNull(deathPlayer);

            killPlayer.KillCount.Value += 1;
            deathPlayer.DeathCount.Value += 1;
          }
        }
      };

      return onKilled;
    }
  }
}

