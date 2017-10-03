﻿using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class FlatGroundMove {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        rigid.AddForce(_inputVec * _force, ForceMode2D.Impulse);

        _actFlag = false;
        _inputVec.x = 0;
      }
    }

    public void MoveLeft() {
      _actFlag = true;
      _inputVec.x -= 1;

      if (_inputVec.x < -1)
        _inputVec.x = -1;
    }

    public void MoveRight() {
      _actFlag = true;
      _inputVec.x += 1;

      if (_inputVec.x > 1)
        _inputVec.x = 1;
    }

    public void SetForce(float force) {
      _force = force;
    }

    protected float _force = 4.0f;
    protected bool _actFlag;
    protected Vector2 _inputVec;
  }
}
