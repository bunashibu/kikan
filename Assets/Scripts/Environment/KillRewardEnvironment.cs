using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public static class KillRewardEnvironment {
    public static KillReward GetRewardFrom(IKillRewardGiver giver, int teamSize) {
      float rewardRatio = 1;

      if (teamSize == 1)
        rewardRatio = 0.7f;
      if (teamSize == 2)
        rewardRatio = 0.6f;

      var killReward = new KillReward();

      killReward.MainExp  = (int)(giver.KillExp  * rewardRatio);
      killReward.MainGold = (int)(giver.KillGold * rewardRatio);

      if (teamSize > 0) {
        killReward.SubExp  = (int)(giver.KillExp  * ((1.0f - rewardRatio) / teamSize));
        killReward.SubGold = (int)(giver.KillGold * ((1.0f - rewardRatio) / teamSize));
      }

      return killReward;
    }

    public static void GiveMainRewardTo(IKillRewardTaker taker, KillReward killReward) {
      taker.Exp.Add(killReward.MainExp);
      taker.Gold.Add(killReward.MainGold);
    }

    public static void GiveSubRewardTo(IKillRewardTaker taker, KillReward killReward) {
      taker.Exp.Add(killReward.SubExp);
      taker.Gold.Add(killReward.SubGold);
    }
  }

  public struct KillReward {
    public int MainExp;
    public int MainGold;
    public int SubExp;
    public int SubGold;
  }
}
