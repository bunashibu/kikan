using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Gold : IntReactiveGauge {
    public Gold(int initialGold, int maxGold) {
      _cur.Value = initialGold;
      _min.Value = 0;
      _max.Value = maxGold;
    }
  }
}

