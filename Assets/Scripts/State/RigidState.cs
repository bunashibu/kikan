using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RigidState : MonoBehaviour {
  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer(_groundLayerName))
      Ground = true;
  }

  void OnCollisionExit2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer(_groundLayerName))
      Ground = false;
  }

  public bool Ground { get; private set; }

  public bool Air {
    get {
      return !Ground && !Ladder;
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
  [SerializeField] private string _groundLayerName;
  [SerializeField] private LayerMask _ladderLayer;
}

