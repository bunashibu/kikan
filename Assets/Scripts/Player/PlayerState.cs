using UnityEngine;
using System.Collections;

public class PlayerState {
  public PlayerState(Collider2D colliderCenter, Collider2D colliderFoot) {
    _colliderCenter        = colliderCenter;
    _colliderFoot          = colliderFoot;

    _groundLayer           = LayerMask.GetMask("Ground");
    _ladderLayer           = LayerMask.GetMask("Ladder");
    _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
    _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
    _portalLayer           = LayerMask.GetMask("Portal");
  }

  public bool Ground           { get { return _colliderFoot.IsTouchingLayers(_groundLayer);             }  }
  public bool Air              { get { return !Ground;                                                  }  }
  public bool Ladder           { get { return _colliderCenter.IsTouchingLayers(_ladderLayer);           }  }
  public bool LadderTopEdge    { get { return _colliderFoot.IsTouchingLayers(_ladderTopEdgeLayer);      }  }
  public bool LadderBottomEdge { get { return _colliderCenter.IsTouchingLayers(_ladderBottomEdgeLayer); }  }
  public bool Portal           { get { return _colliderCenter.IsTouchingLayers(_portalLayer);           }  }

  public bool Slow { get; set; }
  public bool Heavy { get; set; }
  public bool Rigor { get; set; }

  private Collider2D _colliderCenter;
  private Collider2D _colliderFoot;
  private LayerMask _groundLayer;
  private LayerMask _ladderLayer;
  private LayerMask _ladderTopEdgeLayer;
  private LayerMask _ladderBottomEdgeLayer;
  private LayerMask _portalLayer;
}

