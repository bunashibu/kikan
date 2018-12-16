using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LadderJump {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          rigid.AddForce(Vector2.up * 280.0f);
        });
      }
    }

    public void FixedUpdate(Rigidbody2D rigid, Core core) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          float ratio = (float)((core.GetValue(CoreType.Speed) + 100) / 100.0);
          rigid.AddForce(Vector2.up * 280.0f * ratio);
        });
      }
    }

    private void ActuallyMove(Rigidbody2D rigid, Action action) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);

      action();

      _actFlag = false;
    }

    public void JumpOff() {
      _actFlag = true;
    }

    protected bool _actFlag;
  }
}

