using UnityEngine;
using System.Collections;

public class StepDownJump {
  public void FixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);
      rigid.AddForce(Vector2.up * 200.0f);
      _actFlag = false;
    }
  }

  public void StepDown() {
    _actFlag = true;
  }

  private bool _actFlag;
}

