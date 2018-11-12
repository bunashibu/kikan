using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class Hp : IntReactiveGauge {
    public Hp(int initialValue) {
      _cur.Value = initialValue;
      _min.Value = 0;
      _max.Value = initialValue;
    }

    public void FullRecover() {
      _cur.Value = _max.Value;
    }
  }
}

