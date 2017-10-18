using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface ICharacter {
    Rigidbody2D    Rigid          { get; }
    Collider2D     LadderCollider { get; }
    Collider2D     FootCollider   { get; }
    CharacterState State          { get; }
  }
}

