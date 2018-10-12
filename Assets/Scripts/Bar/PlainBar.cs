using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public abstract class PlainBar : Bar {
    public void UpdateView(int cur, int max) {
      //Animate(cur, max);
    }

    public override void OnNotify(Notification notification, object[] args) {}
  }
}

