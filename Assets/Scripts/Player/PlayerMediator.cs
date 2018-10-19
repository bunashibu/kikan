using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerMediator : IListener {
    public PlayerMediator(BattlePlayer player) {
      _player   = player;
      _notifier = new Notifier();

      _notifier.Add(_player.Hp);
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.PlayerInstantiated:
          Assert.IsTrue(args.Length == 0);

          _notifier.Notify(Notification.InitializeHp, _player.HpTable[0]);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          _notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2]);

          break;
        case Notification.Died:
          Assert.IsTrue(args.Length == 1);

          var attacker = (Notifier)args[0];
          attacker.Notify(Notification.GetKillReward, _player.KillExp, _player.KillGold);

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          // Increase Exp
          // Increase Gold

          break;
        default:
          break;
      }
    }

    private BattlePlayer _player;
    private Notifier     _notifier;
  }
}

