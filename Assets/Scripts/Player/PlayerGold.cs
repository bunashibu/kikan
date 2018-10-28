using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerGold : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 4);

          int maxGold = (int)args[3];
          Max = maxGold;

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          int gainGold = (int)args[1];
          Add(gainGold);

          _notifier.Notify(Notification.GoldUpdated, Cur);

          break;
        default:
          break;
      }
    }
  }
}

