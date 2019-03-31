using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IPlayerLocation {
    void InitializeFoot(Collider2D footCollider);
    void InitializeCenter(Collider2D centerCollider);
    bool IsGround           { get; }
    bool IsCanNotDownGround { get; }
    bool IsAir              { get; }
    bool IsLadder           { get; }
    bool IsLadderTopEdge    { get; }
    bool IsLadderBottomEdge { get; }
    bool IsPortal           { get; }
    bool IsLeftSlope        { get; }
    bool IsRightSlope       { get; }
    bool IsLeftGround       { get; }
    bool IsRightGround      { get; }
    float GroundAngle       { get; }
    float SlopeAngle        { get; }
  }
}

