using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAirLinearMove {
  public void FixedUpdate() {
    if (_actFlag) {
      float ratio = (float)((_core.Speed + 100) / 100.0);
      _rigid.AddForce(_inputVec * 2.0f * ratio);

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

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private PlayerCore _core;
  private bool _actFlag;
  private Vector2 _inputVec;
}

