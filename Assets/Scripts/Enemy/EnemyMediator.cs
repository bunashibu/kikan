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
        default:
          break;
      }
    }

    public Notifier Notifier { get; private set; }

    private Enemy _enemy;
  }
}

