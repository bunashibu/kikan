using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class LadderJump {
    public void FixedUpdate(Rigidbody2D rigid) {
      if (_actFlag) {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * 280.0f);
        _actFlag = false;
      }
    }

    public void JumpOff() {
      _actFlag = true;
    }

    protected bool _actFlag;
  }
}

