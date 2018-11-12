using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Exp : IntReactiveGauge {
    public Exp(int initialValue) {
      _cur.Value = 0;
      _min.Value = 0;
      _max.Value = initialValue;
    }
  }
}

