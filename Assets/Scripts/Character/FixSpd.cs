using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FixSpd {
    public FixSpd(float v, FixSpdType type) {
      Value = v;
      Type = type;
    }

    public float Value;
    public FixSpdType Type;
  }
}

