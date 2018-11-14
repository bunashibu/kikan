using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public static class LocationJudger {
    public static bool IsGround(Collider2D collider) {
      return collider.IsTouchingLayers(_groundLayer) || IsCanNotDownGround(collider);
    }

    public static bool IsCanNotDownGround(Collider2D collider) {
      return collider.IsTouchingLayers(_canNotDownGroundLayer);
    }

    public static bool IsAir(Collider2D collider) {
      return !IsGround(collider);
    }

    public static bool IsLadder(Collider2D collider) {
      return collider.IsTouchingLayers(_ladderLayer);
    }

    public static bool IsLadderTopEdge(Collider2D collider) {
      return collider.IsTouchingLayers(_ladderTopEdgeLayer);
    }

    public static bool IsLadderBottomEdge(Collider2D collider) {
      return collider.IsTouchingLayers(_ladderBottomEdgeLayer);
    }

    public static bool IsPortal(Collider2D collider) {
      return collider.IsTouchingLayers(_portalLayer);
    }

    private static readonly LayerMask _groundLayer           = LayerMask.GetMask("Ground");
    private static readonly LayerMask _canNotDownGroundLayer = LayerMask.GetMask("CanNotDownGround");
    private static readonly LayerMask _ladderLayer           = LayerMask.GetMask("Ladder");
    private static readonly LayerMask _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
    private static readonly LayerMask _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
    private static readonly LayerMask _portalLayer           = LayerMask.GetMask("Portal");
  }
}

