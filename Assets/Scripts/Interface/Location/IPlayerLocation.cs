using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IPlayerLocation : IGroundLocation {
    void InitializeFoot(Collider2D footCollider);
    void InitializeCenter(Collider2D centerCollider);
    bool IsGround           { get; }
    bool IsCanNotDownGround { get; }
    bool IsAir              { get; }
    bool IsLadder           { get; }
    bool IsLadderTopEdge    { get; }
    bool IsLadderBottomEdge { get; }
    bool IsPortal           { get; }
  }
}

