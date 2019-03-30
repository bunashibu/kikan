using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IEnemyLocation : IGroundLocation {
    void InitializeFoot(Collider2D footCollider);
    bool IsGround { get; }
  }
}

