using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerLevel : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 4);

          Cur = 1;

          int maxLevel = (int)args[2];
          Max = maxLevel;

          break;
        case Notification.ExpIsMax:
          Assert.IsTrue(args.Length == 0);

          Add(1);
          _notifier.Notify(Notification.LevelUp, Cur);

          break;
        default:
          break;
      }
    }
  }
}

