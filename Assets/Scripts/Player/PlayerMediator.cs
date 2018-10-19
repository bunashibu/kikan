using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerMediator : IListener {
    public PlayerMediator(BattlePlayer player) {
      _player = player;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

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
  }
}

