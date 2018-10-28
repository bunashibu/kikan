using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerMediator : Duplexer {
    public PlayerMediator(BattlePlayer player) {
      _player = player;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.PlayerInstantiated:
          Assert.IsTrue(args.Length == 0);

          _notifier.Notify(Notification.InitializeHp, _player.HpTable[0]);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          _notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2], _player);

          if (_player.Hp.Cur == _player.Hp.Min)
            _notifier.Notify(Notification.Killed, args[0], _player);

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          var killReward = (KillReward)args[0];
          var rewardTaker = (IRewardTaker)args[1];

          if (rewardTaker.PhotonView.isMine)
            _notifier.Notify(Notification.GetKillReward, killReward.MainExp, killReward.MainGold);
          else
            _notifier.Notify(Notification.GetKillReward, killReward.SubExp, killReward.SubGold);

          break;
        default:
          break;
      }
    }

    private BattlePlayer _player;
  }
}

