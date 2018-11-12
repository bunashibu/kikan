using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Level : IntReactiveGauge {
    public Level(int initialLevel, int maxLevel) {
      _cur.Value = initialLevel;
      _min.Value = initialLevel;
      _max.Value = maxLevel;
    }

    public void LevelUp() {
      Add(1);
    }
  }
}

