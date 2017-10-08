using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FlatGroundMove {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        rigid.velocity = new Vector2(0, 0);
        rigid.AddForce(_direction * _force);

        _actFlag = false;
        _direction.x = 0;
        _radAngle = 0;
      }
    }

    public void SetForce(float force) {
      _force = force;
    }

    public void MoveLeft(float degAngle) {
      Move(Vector2.left, degAngle);
    }

    public void MoveRight(float degAngle) {
      Vector2 direction = Vector2.right;
      Move(direction, degAngle);
    }

    private void Move(Vector2 direction, float degAngle) {
      _actFlag = true;
      _radAngle = degAngle * Mathf.Deg2Rad;
      _direction.x = direction.x * Mathf.Cos(_radAngle);
      _direction.y = direction.y * Mathf.Sin(_radAngle);
    }

    protected float _force = 4.0f;
    protected float _radAngle = 0;
    protected bool _actFlag;
    protected Vector2 _direction;
  }
}

