using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAirLinearMove {
  public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
    if (_actFlag) {
      float ratio = (float)((core.Speed + 100) / 100.0);
      rigid.AddForce(_inputVec * 2.0f * ratio);

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

  private bool _actFlag;
  private Vector2 _inputVec;
}

