using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreClimbJump {
  public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
    if (_actFlag) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);

      float ratio = (float)((core.Speed + 100) / 100.0);
      rigid.AddForce(Vector2.up * 280.0f * ratio);

      _actFlag = false;
    }
  }

  public void JumpOff() {
    _actFlag = true;
  }

  private bool _actFlag;
}

