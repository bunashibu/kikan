using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerExp : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 2);

          int initialMaxExp = (int)args[1];
          Cur = 0;
          Max = initialMaxExp;

          _notifier.Notify(Notification.ExpUpdated, Cur, Max);

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          int gainExp = (int)args[0];
          Add(gainExp);

          _notifier.Notify(Notification.ExpUpdated, Cur, Max);

          break;
        case Notification.LevelUp:
          Assert.IsTrue(args.Length == 1);

          int maxExp = (int)args[0];
          Cur = 0;
          Max = maxExp;

          _notifier.Notify(Notification.ExpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }
  }
}

