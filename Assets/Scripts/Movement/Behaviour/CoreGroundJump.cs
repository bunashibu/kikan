using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreGroundJump : GroundJump {
    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);

        float ratio = (float)((core.Speed + 100) / 100.0);
        rigid.AddForce(Vector2.up * (_force * 0.02f) * ratio, ForceMode2D.Impulse);

        _actFlag = false;
      }
    }
  }
}

