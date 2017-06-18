using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGroundLinearMove {
  public void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(0, _rigid.velocity.y); // like Aizen

      float ratio = (float)((_core.Speed + 100) / 100.0);
      _rigid.AddForce(_inputVec * _force * ratio);

      _actFlag = false;
      _inputVec.x = 0;
    }
  }

  public void MoveLeft() {
    _actFlag = true;
    _inputVec.x -= 1;

    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight() {
    _actFlag = true;
    _inputVec.x += 1;

    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private PlayerCore _core;
  private float _force = 60.0f;
  private bool _actFlag;
  private Vector2 _inputVec;
}

