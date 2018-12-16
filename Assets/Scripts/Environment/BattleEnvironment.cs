using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public static class BattleEnvironment {
    public static Action<IAttacker, int, bool, HitEffectType> OnAttacked(IOnAttacked target,
                                                                         Action<int, bool, int, IPhotonBehaviour> PopupNumber,
                                                                         Action<HitEffectType, IPhotonBehaviour> PopupHitEffect) {
      Action<IAttacker, int, bool, HitEffectType> onAttacked = (attacker, damage, isCritical, hitEffectType) => {
        target.Hp.Subtract(damage);

        int damageSkin = 0;
        if (attacker is IDamageSkin)
          damageSkin = ((IDamageSkin)attacker).DamageSkinId;

        if (target is IPhotonBehaviour) {
          var targetPhoton = (IPhotonBehaviour)target;
          PopupHitEffect(hitEffectType, targetPhoton);
          PopupNumber(damage, isCritical, damageSkin, targetPhoton);
        }

        if (target.Hp.Cur.Value == target.Hp.Min.Value)
          target.OnKilled(attacker);
      };

      return onAttacked;
    }

    public static Action<IAttacker> OnKilled(IOnAttacked target,
                                             Func<IKillRewardGiver, int, KillReward> GetRewardFrom,
                                             Action<IKillRewardTaker, KillReward> GiveRewardTo) {
      Action<IAttacker> onKilled = (attacker) => {
        if (attacker is IKillRewardTaker && target is IKillRewardGiver) {
          var taker = (IKillRewardTaker)attacker;
          var giver = (IKillRewardGiver)target;

          GiveRewardTo(taker, GetRewardFrom(giver, taker.Teammates.Count));
        }

        if (attacker is IPlayer && target is IPlayer) {
          var killPlayer  = (IPlayer)attacker;
          var deathPlayer = (IPlayer)target;

          killPlayer.KillCount.Value += 1;
          deathPlayer.DeathCount.Value += 1;
        }
      };

      return onKilled;
    }

    public static Action<DebuffType, float> OnDebuffed(IOnDebuffed target) {
      Action<DebuffType, float> onDebuffed = (debuffType, duration) => {
        target.Debuff.DurationEnable(debuffType, duration);
      };

      return onDebuffed;
    }
  }
}

