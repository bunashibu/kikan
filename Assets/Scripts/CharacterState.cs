using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CharacterState {
    public float GroundAngle { get; set; }
    public bool GroundLeft  { get; set; }
    public bool GroundRight { get; set; }

    public bool Rigor      { get; set; }
    public bool Invincible { get; set; }
  }
}

