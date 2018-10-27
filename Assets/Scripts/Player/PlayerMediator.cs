using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerMediator : IResponder {
    public PlayerMediator(BattlePlayer player) {
      Notifier = new Notifier();
      _player  = player;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.PlayerInstantiated:
          Assert.IsTrue(args.Length == 0);

          Notifier.Notify(Notification.InitializeHp, _player.HpTable[0]);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          Notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2], _player);

          if (_player.Hp.Cur == _player.Hp.Min)
            Notifier.Notify(Notification.Killed, args[0], _player);

          break;
        default:
          break;
      }
    }

    public Notifier Notifier { get; private set; }

    private BattlePlayer _player;
  }
}

