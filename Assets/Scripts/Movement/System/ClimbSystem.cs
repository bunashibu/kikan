using UnityEngine;
using System.Collections;

public class ClimbSystem : ScriptableObject {
  public void CallFixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      rigid.AddForce(_inputVec * 30.0f);
      _actFlag = false;
    }
  }

  public void MoveUp() {
    _actFlag = true;
    _inputVec.y += 1;

    if (_inputVec.y > 1)
      _inputVec.y = 1;
  }

  public void MoveDown() {
    _actFlag = true;
    _inputVec.y -= 1;

    if (_inputVec.y < -1)
      _inputVec.y = -1;
  }

  private bool _actFlag;
  private Vector2 _inputVec;
}

