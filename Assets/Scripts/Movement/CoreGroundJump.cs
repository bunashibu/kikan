using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGroundJump {
  public void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(_rigid.velocity.x, 0);

      float ratio = (float)((_core.Speed + 100) / 100.0);
      _rigid.AddForce(Vector2.up * _force * ratio);

      _actFlag = false;
    }
  }

  public void Jump() {
    _actFlag = true;
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private PlayerCore _core;
  private float _force = 300.0f;
  private bool _actFlag;
}

