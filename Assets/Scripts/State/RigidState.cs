using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RigidState : MonoBehaviour {
  public bool Ground {
    get {
      return _colliderFoot.IsTouchingLayers(_groundLayer);
    }
  }

  public bool Air {
    get {
      return !Ground;
    }
  }

  public bool Ladder {
    get {
      return _colliderBody.IsTouchingLayers(_ladderLayer);
    }
  }

  public bool Slow { get; set; }
  public bool Heavy { get; set; }
  public bool Immobile { get; set; }

  [SerializeField] private BoxCollider2D _colliderBody;
  [SerializeField] private BoxCollider2D _colliderFoot;
  [SerializeField] private LayerMask _groundLayer;
  [SerializeField] private LayerMask _ladderLayer;
}

