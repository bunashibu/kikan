using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IEnemyLocation {
    void InitializeFoot(Collider2D footCollider);
    bool IsGround      { get; }
    bool IsLeftSlope   { get; }
    bool IsRightSlope  { get; }
    bool IsLeftGround  { get; }
    bool IsRightGround { get; }
    float GroundAngle  { get; }
    float SlopeAngle   { get; }
  }
}

