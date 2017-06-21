using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RigidState : MonoBehaviour {
  public bool Ground           { get { return _colliderFoot.IsTouchingLayers(_groundLayer);             }  }
  public bool Air              { get { return !Ground;                                                  }  }
  public bool Ladder           { get { return _colliderCenter.IsTouchingLayers(_ladderLayer);           }  }
  public bool LadderTopEdge    { get { return _colliderFoot.IsTouchingLayers(_ladderTopEdgeLayer);      }  }
  public bool LadderBottomEdge { get { return _colliderCenter.IsTouchingLayers(_ladderBottomEdgeLayer); }  }
  public bool Portal           { get { return _colliderCenter.IsTouchingLayers(_portalLayer);           }  }

  public bool Slow { get; set; }
  public bool Heavy { get; set; }
  public bool Rigor { get; set; }

  [SerializeField] private BoxCollider2D _colliderCenter;
  [SerializeField] private BoxCollider2D _colliderFoot;
  [SerializeField] private LayerMask _groundLayer;
  [SerializeField] private LayerMask _ladderLayer;
  [SerializeField] private LayerMask _ladderTopEdgeLayer;
  [SerializeField] private LayerMask _ladderBottomEdgeLayer;
  [SerializeField] private LayerMask _portalLayer;
}

