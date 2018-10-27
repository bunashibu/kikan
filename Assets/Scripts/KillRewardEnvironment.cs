using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class KillRewardEnvironment : SingletonMonoBehaviour<KillRewardEnvironment> {
    void Awake() {
      base.Awake();
      _notifier = new Notifier();
    }

    public void OnNotify(Notification notification, object[] args) {
      if (notification == Notification.Killed) {
        Assert.IsTrue(args.Length == 2);

        var rewardTaker = ((GameObject)args[0]).GetComponent<IRewardTaker>();
        Assert.IsNotNull(rewardTaker);

        _notifier.Add(rewardTaker.OnNotify);
        rewardTaker.Teammates.ForEach(teammate => _notifier.Add(teammate.OnNotify) );

        var killedOne = (IBattle)args[1];
        _notifier.Notify(Notification.GetKillReward, GetKillRewardFrom(killedOne, rewardTaker.Teammates.Count), rewardTaker);

        _notifier.RemoveAll();
      }
    }

    private KillReward GetKillRewardFrom(IBattle target, int teamSize) {
      double rewardRatio = 1;

      if (teamSize == 1)
        rewardRatio = 0.7;
      if (teamSize == 2)
        rewardRatio = 0.6;

      var killReward = new KillReward();

      killReward.MainExp  = (int)(target.KillExp  * rewardRatio);
      killReward.MainGold = (int)(target.KillGold * rewardRatio);

      if (teamSize > 0) {
        killReward.SubExp  = (int)(target.KillExp  * ((1.0 - rewardRatio) / teamSize));
        killReward.SubGold = (int)(target.KillGold * ((1.0 - rewardRatio) / teamSize));
      }

      return killReward;
    }

    private Notifier _notifier;
  }

  public class KillReward {
    public int MainExp  = 0;
    public int MainGold = 0;
    public int SubExp   = 0;
    public int SubGold  = 0;
  }
}

