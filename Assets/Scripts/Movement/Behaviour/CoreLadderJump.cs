using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreLadderJump : LadderJump {
    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        float ratio = (float)((core.Speed + 100) / 100.0);
        rigid.AddForce(Vector2.up * 280.0f * ratio);

        _actFlag = false;
      }
    }
  }
}

