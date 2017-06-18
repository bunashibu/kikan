using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGroundJump {
  public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
    if (_actFlag) {
      rigid.velocity = new Vector2(rigid.velocity.x, 0);

      float ratio = (float)((core.Speed + 100) / 100.0);
      rigid.AddForce(Vector2.up * _force * ratio);

      _actFlag = false;
    }
  }

  public void Jump() {
    _actFlag = true;
  }

  public void SetForce(float force) {
    _force = force;
  }

  private float _force = 300.0f;
  private bool _actFlag;
}

