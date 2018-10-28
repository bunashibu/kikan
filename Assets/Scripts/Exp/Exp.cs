using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class Exp : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 2);

          int maxExp = (int)args[1];
          Cur = 0;
          Max = maxExp;

          break;
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          int gainExp = (int)args[0];
          Add(gainExp);

          break;
        default:
          break;
      }
    }
  }
}

