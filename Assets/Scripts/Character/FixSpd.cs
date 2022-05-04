using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FixSpd {
    // TODO: Character default jump value should be static
    public FixSpd(float v, FixSpdType type, float ladderRatio = 1, float jumpValue = 400.0f) {
      Value = v;
      LadderRatio = ladderRatio;
      JumpValue = jumpValue;
      Type = type;
    }

    public float Value;
    public float LadderRatio;
    public float JumpValue;
    public FixSpdType Type;
  }
}
