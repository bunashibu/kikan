using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IPlayerLocationJudger {
    void InitializeFootJudge(Collider2D footCollider);
    void InitializeCenterJudge(Collider2D centerCollider);

    bool IsGround           { get; }
    bool IsCanNotDownGround { get; }
    bool IsAir              { get; }
    bool IsLadder           { get; }
    bool IsLadderTopEdge    { get; }
    bool IsLadderBottomEdge { get; }
    bool IsPortal           { get; }
  }
}

