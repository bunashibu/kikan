using UnityEngine;
using System.Collections;

public class GroundJump {
  public void FixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      rigid.AddForce(Vector2.up * (_force * 0.03f), ForceMode2D.Impulse);

      _actFlag = false;
    }
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

