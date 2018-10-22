using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyHp : Hp {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.InitializeHp:
          Assert.IsTrue(args.Length == 1);

          Cur = (int)args[0];
          Max = (int)args[0];

          Notifier.Send(Notification.HpUpdated, Cur, Max);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          int damage = (int)args[0];
          Subtract(damage);

          Notifier.Send(Notification.HpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }
  }
}

