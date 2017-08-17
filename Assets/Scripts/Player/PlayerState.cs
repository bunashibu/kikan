using UnityEngine;
using System.Collections;

public class PlayerState {
  public PlayerState(BoxCollider2D ladderCollider, FootCollider footCollider) {
    _ladderCollider        = ladderCollider;
    _footCollider          = footCollider;

    _groundLayer           = LayerMask.GetMask("Ground");
    _ladderLayer           = LayerMask.GetMask("Ladder");
    _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
    _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
    _portalLayer           = LayerMask.GetMask("Portal");
  }

  public bool Ground           { get { return _footCollider.IsTouchingLayers(_groundLayer);             }  }
  public bool Air              { get { return !Ground;                                                  }  }
  public bool Ladder           { get { return _ladderCollider.IsTouchingLayers(_ladderLayer);           }  }
  public bool LadderTopEdge    { get { return _footCollider.IsTouchingLayers(_ladderTopEdgeLayer);      }  }
  public bool LadderBottomEdge { get { return _ladderCollider.IsTouchingLayers(_ladderBottomEdgeLayer); }  }
  public bool Portal           { get { return _ladderCollider.IsTouchingLayers(_portalLayer);           }  }

  public bool Slow { get; set; }
  public bool Heavy { get; set; }
  public bool Rigor { get; set; }

  private Collider2D _ladderCollider;
  private FootCollider _footCollider;
  private LayerMask _groundLayer;
  private LayerMask _ladderLayer;
  private LayerMask _ladderTopEdgeLayer;
  private LayerMask _ladderBottomEdgeLayer;
  private LayerMask _portalLayer;
}

