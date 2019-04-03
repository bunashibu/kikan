using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CurSkillCT : FloatReactiveGauge {
    public CurSkillCT(float max) {
      _min.Value = 0;
      _cur.Value = 0;
      _max.Value = max;
    }

    public void SetFullTime() {
      _cur.Value = _max.Value;
    }
  }
}

