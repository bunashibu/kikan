using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Character2D : MonoBehaviour {
  void Update() {
    UpdateRaycast();
  }

  private void UpdateRaycast() {
    Vector2 footRayOrigin = new Vector2(_character.Collider.bounds.center.x, _character.Collider.bounds.min.y);
    RaycastHit2D hit = Physics2D.Raycast(footRayOrigin, Vector2.down, Mathf.Abs(_character.Rigid.velocity.y), _groundMask);
  }

  [SerializeField] private ICharacter _character;
  [SerializeField] private LayerMask _groundMask;

  [Range(0, 1)]
  [SerializeField] private float _friction;
}

