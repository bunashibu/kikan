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

    public static Action<IBattle> OnKilled(IBattle target, Func<IBattle, int, KillReward> GetRewardFrom, Action<IRewardTaker, KillReward> GiveRewardTo) {
      Action<IBattle> onKilled = (attacker) => {
        if (attacker.Tag == "Player") {
          var rewardTaker = attacker.gameObject.GetComponent<IRewardTaker>();
          Assert.IsNotNull(rewardTaker);

          GiveRewardTo(rewardTaker, GetRewardFrom(target, rewardTaker.Teammates.Count));

          if (target.Tag == "Player") {
            var killCounter  = attacker.gameObject.GetComponent<IKillDeathCounter>();
            var deathCounter = target.gameObject.GetComponent<IKillDeathCounter>();

            Assert.IsNotNull(killCounter);
            Assert.IsNotNull(deathCounter);

            killCounter.KillCount.Value += 1;
            deathCounter.DeathCount.Value += 1;
          }
        }
      };

      return onKilled;
    }
  }
}

