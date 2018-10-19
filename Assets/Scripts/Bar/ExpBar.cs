using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class ExpBar : Bar {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.ExpUpdated:
          Assert.IsTrue(args.Length == 2);

          _view.UpdateView((int)args[0], (int)args[1]); // cur, max
          break;
        default:
          break;
      }
    }
  }
}

