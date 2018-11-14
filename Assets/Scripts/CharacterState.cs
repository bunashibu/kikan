using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CharacterState {
    public CharacterState(Collider2D ladderCollider, Collider2D footCollider) {
      _ladderCollider        = ladderCollider;
      _footCollider          = footCollider;

      _groundLayer           = LayerMask.GetMask("Ground");
      _canNotDownGroundLayer = LayerMask.GetMask("CanNotDownGround");
      _ladderLayer           = LayerMask.GetMask("Ladder");
      _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
      _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
      _portalLayer           = LayerMask.GetMask("Portal");
    }

    public float GroundAngle { get; set; }
    public bool GroundLeft  { get; set; }
    public bool GroundRight { get; set; }

    //public bool Ground           => _footCollider.IsTouchingLayers(_groundLayer) || CanNotDownGround;
    public bool CanNotDownGround => _footCollider.IsTouchingLayers(_canNotDownGroundLayer);
    //public bool Air              => !Ground;
    public bool Ladder           => _ladderCollider.IsTouchingLayers(_ladderLayer);
    public bool LadderTopEdge    => _footCollider.IsTouchingLayers(_ladderTopEdgeLayer);
    public bool LadderBottomEdge => _ladderCollider.IsTouchingLayers(_ladderBottomEdgeLayer);
    public bool Portal           => _ladderCollider.IsTouchingLayers(_portalLayer);

    public bool Rigor      { get; set; }
    public bool Invincible { get; set; }

    private Collider2D _ladderCollider;
    private Collider2D _footCollider;
    private LayerMask _groundLayer;
    private LayerMask _canNotDownGroundLayer;
    private LayerMask _ladderLayer;
    private LayerMask _ladderTopEdgeLayer;
    private LayerMask _ladderBottomEdgeLayer;
    private LayerMask _portalLayer;
  }
}

