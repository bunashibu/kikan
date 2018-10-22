using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerHp : Hp {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.InitializeHp:
          Assert.IsTrue(args.Length == 1);

          Cur = (int)args[0];
          Max = (int)args[0];

          Notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          int damage = (int)args[0];
          Subtract(damage);

          Notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }
  }
}

