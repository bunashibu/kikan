using UnityEngine;
using System.Collections;

public class Climb {
  public void FixedUpdate(Transform trans) {
    if (_actFlag) {
      trans.Translate(_inputVec * 0.04f);
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
  private Vector3 _inputVec;
}

