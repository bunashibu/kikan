using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CharacterState {
    public void ToGround() {
      IsGround = true;
      IsAir    = false;
    }

    public void ToAir() {
      IsGround = false;
      IsAir    = true;
    }

    public void OnSlope() {
      IsSlope = true;
    }

    public void OffSlope() {
      IsSlope = false;
    }

    public bool IsGround { get; private set; }
    public bool IsAir    { get; private set; }
    public bool IsSlope  { get; private set; }
  }
}

