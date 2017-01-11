using UnityEngine;
using System.Collections;

public class LieDownSystem : MonoBehaviour {
  public void LieDown() {
    _collider.offset = new Vector2(0.0f, -0.2f);
    _collider.size = new Vector2(0.9f, 0.6f);
  }

  public void Stay() {
    _collider.offset = new Vector2(0.0f, -0.05f);
    _collider.size = new Vector2(0.6f, 0.9f);
  }

  [SerializeField] private BoxCollider2D _collider;
}

