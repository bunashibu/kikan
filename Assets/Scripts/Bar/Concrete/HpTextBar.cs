using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HpTextBar : TextBar {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.HpAdd:
          UpdateView((int)args[0], (int)args[1]); // cur, max
          break;

        case Notification.HpSubtract:
          UpdateView((int)args[0], (int)args[1]); // cur, max
          break;

        default:
          break;
      }
    }
  }
}

