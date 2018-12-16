using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class GroundJump {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          rigid.AddForce(Vector2.up * (_force * 0.02f), ForceMode2D.Impulse);
        });
      }
    }

    public void FixedUpdate(Rigidbody2D rigid, Core core) {
      if (_actFlag) {
        ActuallyMove(rigid, () => {
          float ratio = (float)((core.GetValue(CoreType.Speed) + 100) / 100.0);
          rigid.AddForce(Vector2.up * (_force * 0.02f) * ratio, ForceMode2D.Impulse);
        });
      }
    }

    private void ActuallyMove(Rigidbody2D rigid, Action action) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);

      action();

      _actFlag = false;
    }

    public void Jump() {
      _actFlag = true;
    }

    public void SetForce(float force) {
      _force = force;
    }

    protected float _force = 300.0f;
    protected bool _actFlag;
  }
}

