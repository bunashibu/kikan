using UnityEngine;
using System.Collections;

public class Climb : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _trans.Translate(_inputVec * 0.05f);
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

  [SerializeField] private Transform _trans;
  private bool _actFlag;
  private Vector3 _inputVec;
}

