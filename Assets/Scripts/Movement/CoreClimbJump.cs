using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreClimbJump {
  public void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(_rigid.velocity.x, 0);

      float ratio = (float)((_core.Speed + 100) / 100.0);
      _rigid.AddForce(Vector2.up * 280.0f * ratio);

      _actFlag = false;
    }
  }

  public void JumpOff() {
    _actFlag = true;
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private PlayerCore _core;
  private bool _actFlag;
}

