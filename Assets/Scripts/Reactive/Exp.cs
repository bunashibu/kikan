using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Exp : IntReactiveGauge {
    public Exp(int initialNeedExp) {
      _cur.Value = 0;
      _min.Value = 0;
      _max.Value = initialNeedExp;
    }

    public void Update(int needExp) {
      _cur.Value = 0;
      _max.Value = needExp;
    }
  }
}

