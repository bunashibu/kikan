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
    RaycastHit2D hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down, Mathf.Abs(_character.Rigid.velocity.y), _groundMask);

    if (hitGround.collider != null) {
      Debug.DrawRay(footRayOrigin, Vector2.down * Mathf.Abs(_character.Rigid.velocity.y), Color.red);
      CalculateGroundDistance(hitGround);
    } else {
      _character.Rigid.gravityScale = 1;
    }
  }

  private void CalculateGroundDistance(RaycastHit2D hitGround) {
    if (hitGround.distance == 0) {
      _character.Rigid.velocity = new Vector2(_character.Rigid.velocity.x, 0);
      _character.Rigid.gravityScale = 0;
      transform.position = new Vector3(transform.position.x, hitGround.collider.bounds.max.y + _character.Collider.bounds.extents.y, 0);
    } else {
      _character.Rigid.gravityScale = 1;
    }
  }

  [SerializeField] private ICharacter _character;
  [SerializeField] private LayerMask _groundMask;

  [Range(0, 1)]
  [SerializeField] private float _friction;
}

