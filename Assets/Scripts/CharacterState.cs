using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CharacterState {
    public bool IsGround { get; private set; }
    public bool IsAir    { get; private set; }

    [System.Obsolete]
    public bool Ground { get { return IsGround; } }
    [System.Obsolete]
    public bool Air    { get { return IsAir;    } }

    public float GroundAngle {
      get; set;
    }

    public bool GroundLeft {
      get; set;
    }

    public bool GroundRight {
      get; set;
    }
  }
}

