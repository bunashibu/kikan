using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ExpBar : Bar {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.ExpUpdated:
          _view.UpdateView((int)args[0], (int)args[1]); // cur, max
          break;
        default:
          break;
      }
    }
  }
}

