using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlainBarView : BarView {
    public override void UpdateView(int cur, int max) {
      Animate(cur, max);
    }
  }
}

