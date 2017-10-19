using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Rigidbody2D))]
  [RequireComponent(typeof(Collider2D))]
  public class Character2D : MonoBehaviour {
    void Awake() {
      _character = gameObject.GetComponent<ICharacter>();
    }

    void Update() {
      UpdateGroundRaycast();
    }

    private void UpdateGroundRaycast() {
      Vector2 footRayOrigin = new Vector2(_character.FootCollider.bounds.center.x, _character.FootCollider.bounds.min.y);

      float rayLength = 0.1f + Mathf.Abs(_character.Rigid.velocity.y) * Time.deltaTime;
      RaycastHit2D hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down, rayLength, _groundMask);

      if (_character.State.Ground) {
        float degAngle = Vector2.Angle(hitGround.normal, Vector2.up);
        if (degAngle == 90)
          degAngle = 0;
        _character.State.GroundAngle = degAngle;

        if (degAngle > 0 && degAngle < 90) {
          float sign = Mathf.Sign(hitGround.normal.x);
          _character.State.GroundLeft  = (sign == 1 ) ? true : false;
          _character.State.GroundRight = (sign == -1) ? true : false;
        } else {
          _character.State.GroundLeft  = false;
          _character.State.GroundRight = false;
        }
      }

      Debug.DrawRay(footRayOrigin, Vector2.down * rayLength, Color.red);
    }

    [SerializeField] private LayerMask _groundMask;
    private ICharacter _character;
  }
}

