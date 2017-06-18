using UnityEngine;
using System.Collections;

public class AirLinearMove {
  public void FixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      rigid.AddForce(_inputVec * 2.0f);

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

