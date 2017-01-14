using UnityEngine;
using System.Collections;

public class RigidState : MonoBehaviour {
  public bool Ground {
    get {
      return _colliderFoot.IsTouchingLayers(_groundLayer);
    }
  }

  public bool Air { get; private set; }

  public bool Stand {
    get {
      return true;
    }
  }

  public bool LieDown { get; private set; }
  public bool Ladder { get; private set; }
  public bool Slow { get; private set; }
  public bool Heavy { get; private set; }
  public bool Immobile { get; private set; }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _colliderBody;
  [SerializeField] private BoxCollider2D _colliderFoot;
  [SerializeField] private LayerMask _groundLayer;
  [SerializeField] private LayerMask _LadderLayer;
}

