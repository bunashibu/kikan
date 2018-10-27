using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyMediator : IResponder {
    public EnemyMediator(Enemy enemy) {
      Notifier = new Notifier();
      _enemy = enemy;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.EnemyInstantiated:
          Assert.IsTrue(args.Length == 0);

          Notifier.Notify(Notification.InitializeHp, _enemy.Data.Hp);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          Notifier.Notify(Notification.TakeDamage, args[0], args[1], args[2], _enemy);

          if (_enemy.Hp.Cur == _enemy.Hp.Min)
            Notifier.Notify(Notification.Killed, args[0], _enemy);

          break;
        default:
          break;
      }
    }

    public Notifier Notifier { get; private set; }

    private Enemy _enemy;
  }
}

