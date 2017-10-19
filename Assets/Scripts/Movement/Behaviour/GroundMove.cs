using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class GroundMove {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        rigid.velocity = new Vector2(0, 0);
        rigid.AddForce(_direction * _force, ForceMode2D.Impulse);

        _actFlag = false;
        _direction.x = 0;
      }
    }

    public void SetForce(float force) {
      _force = force;
    }

    public void MoveLeft(float degAngle) {
      Move(Vector2.left, degAngle);
    }

    public void MoveRight(float degAngle) {
      Move(Vector2.right, degAngle);
    }

    private void Move(Vector2 direction, float degAngle) {
      _actFlag = true;

      float radAngle = degAngle * Mathf.Deg2Rad;
      _direction.x = direction.x * Mathf.Cos(radAngle);
      _direction.y = Mathf.Sin(radAngle);
    }

    protected float _force = 4.0f;
    protected bool _actFlag;
    protected Vector2 _direction;
  }
}

