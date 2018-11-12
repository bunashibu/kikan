using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerMediator : Duplexer {
    public PlayerMediator(BattlePlayer player) {
      _player = player;
    }

    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.PlayerInstantiated:
          Assert.IsTrue(args.Length == 0);

          _notifier.Notify(Notification.Initialize, _player.HpTable[0], _player.ExpTable[0], _player.MaxLevel, _player.MaxGold);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          _notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2], _player);

          if (_player.Hp.Cur.Value == _player.Hp.Min.Value)
            _notifier.Notify(Notification.Killed, args[0], _player);

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          var killReward = (KillReward)args[0];
          var rewardTaker = (IBattle)args[1];

          if (rewardTaker.PhotonView.isMine)
            _notifier.Notify(Notification.GetKillReward, killReward.MainExp, killReward.MainGold);
          else
            _notifier.Notify(Notification.GetKillReward, killReward.SubExp, killReward.SubGold);

          if (_player.Exp.Cur == _player.Exp.Max)
            _notifier.Notify(Notification.ExpIsMax);

          break;
        case Notification.LevelUp:
          Assert.IsTrue(args.Length == 1);

          // NOTE: level 1 represents index 0
          int level = (int)args[0];
          _notifier.Notify(Notification.LevelUp, _player.HpTable[level-1], _player.ExpTable[level-1]);

          break;
        default:
          break;
      }
    }

    private BattlePlayer _player;
  }
}

