using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LadderJump {
    // 4.0f is GroundMove force

    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          rigid.AddForce(_direction * 4.0f, ForceMode2D.Impulse);
          rigid.AddForce(Vector2.up * 280.0f);
        });
      }
    }

    public void FixedUpdate(Rigidbody2D rigid, Core core) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          float ratio = (float)((core.GetValue(CoreType.Speed) + 100) / 100.0);
          rigid.AddForce(_direction * 4.0f * ratio, ForceMode2D.Impulse);
          rigid.AddForce(Vector2.up * 280.0f * ratio);
        });
      }
    }

    private void ActuallyMove(Rigidbody2D rigid, Action action) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);

      action();

      _actFlag = false;
    }

    public void JumpOff(Vector2 direction) {
      _actFlag = true;
      _direction = direction;
    }

    protected bool _actFlag;
    private Vector2 _direction;
  }
}
