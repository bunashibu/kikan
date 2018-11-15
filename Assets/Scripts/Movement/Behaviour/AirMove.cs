using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class AirMove {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        ActuallyMove(() => {
          rigid.AddForce(_inputVec * 2.0f);
        });
      }
    }

    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        ActuallyMove(() => {
          float ratio = (float)((core.Speed + 100) / 100.0);
          rigid.AddForce(_inputVec * 2.0f * ratio);
        });
      }
    }

    private void ActuallyMove(Action action) {
      action();

      _actFlag = false;
      _inputVec.x = 0;
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

    protected bool _actFlag;
    protected Vector2 _inputVec;
  }
}

