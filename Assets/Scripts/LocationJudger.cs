using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LocationJudger : IPlayerLocationJudger, IEnemyLocationJudger {
    public void InitializeFootJudge(Collider2D footCollider) {
      _footCollider = footCollider;
    }

    public void InitializeCenterJudge(Collider2D centerCollider) {
      _centerCollider = centerCollider;
    }

    public bool IsGround           => _footCollider.IsTouchingLayers(_groundLayer) || IsCanNotDownGround;
    public bool IsCanNotDownGround => _footCollider.IsTouchingLayers(_canNotDownGroundLayer);
    public bool IsAir              => !IsGround;
    public bool IsLadder           => _centerCollider.IsTouchingLayers(_ladderLayer);
    public bool IsLadderTopEdge    => _centerCollider.IsTouchingLayers(_ladderTopEdgeLayer);
    public bool IsLadderBottomEdge => _centerCollider.IsTouchingLayers(_ladderBottomEdgeLayer);
    public bool IsPortal           => _centerCollider.IsTouchingLayers(_portalLayer);

    private static readonly LayerMask _groundLayer           = LayerMask.GetMask("Ground");
    private static readonly LayerMask _canNotDownGroundLayer = LayerMask.GetMask("CanNotDownGround");
    private static readonly LayerMask _ladderLayer           = LayerMask.GetMask("Ladder");
    private static readonly LayerMask _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
    private static readonly LayerMask _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
    private static readonly LayerMask _portalLayer           = LayerMask.GetMask("Portal");

    private Collider2D _footCollider;
    private Collider2D _centerCollider;
  }
}

