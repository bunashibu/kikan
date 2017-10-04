using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class PlayerState {
    public PlayerState(BoxCollider2D ladderCollider, BoxCollider2D footCollider) {
      _ladderCollider        = ladderCollider;
      _footCollider          = footCollider;

      _groundLayer           = LayerMask.GetMask("Ground");
      _canNotDownGroundLayer = LayerMask.GetMask("CanNotDownGround");
      _ladderLayer           = LayerMask.GetMask("Ladder");
      _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
      _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
      _portalLayer           = LayerMask.GetMask("Portal");
    }

    public bool Ground {
      get; set;
      /*
      get {
        return _footCollider.IsTouchingLayers(_groundLayer) || CanNotDownGround;
      }
      */
    }

    public bool CanNotDownGround { get { return _footCollider.IsTouchingLayers(_canNotDownGroundLayer);           } }
    public bool Air              { get { return !Ground;                                                          } }
    public bool Ladder           { get { return _ladderCollider.IsTouchingLayers(_ladderLayer);                   } }
    public bool LadderTopEdge    { get { return _footCollider.IsTouchingLayers(_ladderTopEdgeLayer);              } }
    public bool LadderBottomEdge { get { return _ladderCollider.IsTouchingLayers(_ladderBottomEdgeLayer);         } }
    public bool Portal           { get { return _ladderCollider.IsTouchingLayers(_portalLayer);                   } }

    public bool Slow { get; set; }
    public bool Heavy { get; set; }
    public bool Rigor { get; set; }

    private BoxCollider2D _ladderCollider;
    private BoxCollider2D _footCollider;
    private LayerMask _groundLayer;
    private LayerMask _canNotDownGroundLayer;
    private LayerMask _ladderLayer;
    private LayerMask _ladderTopEdgeLayer;
    private LayerMask _ladderBottomEdgeLayer;
    private LayerMask _portalLayer;
  }
}

