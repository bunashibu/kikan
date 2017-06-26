using UnityEngine;
using System.Collections;

public class PlayerState {
  public PlayerState(BoxCollider2D colliderCenter, BoxCollider2D colliderFoot) {
    _colliderCenter        = colliderCenter;
    _colliderFoot          = colliderFoot;

    _groundLayer           = LayerMask.NameToLayer("Ground");
    _ladderLayer           = LayerMask.NameToLayer("Ladder");
    _ladderTopEdgeLayer    = LayerMask.NameToLayer("LadderTopEdge");
    _ladderBottomEdgeLayer = LayerMask.NameToLayer("LadderBottomEdge");
    _portalLayer           = LayerMask.NameToLayer("Portal");
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

  private BoxCollider2D _colliderCenter;
  private BoxCollider2D _colliderFoot;
  private int _groundLayer;
  private int _ladderLayer;
  private int _ladderTopEdgeLayer;
  private int _ladderBottomEdgeLayer;
  private int _portalLayer;
}

