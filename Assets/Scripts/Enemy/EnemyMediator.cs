using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyMediator : Duplexer {
    public EnemyMediator(Enemy enemy) {
      _enemy = enemy;
    }

    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.EnemyInstantiated:
          Assert.IsTrue(args.Length == 0);

          _notifier.Notify(Notification.Initialize, _enemy.Data.Hp);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          _notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2], _enemy);

          if (_enemy.Hp.Cur == _enemy.Hp.Min)
            _notifier.Notify(Notification.Killed, args[0], _enemy);

          break;
        default:
          break;
      }
    }

    private Enemy _enemy;
  }
}

