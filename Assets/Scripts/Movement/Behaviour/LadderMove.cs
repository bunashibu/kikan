using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class LadderMove {
    public void FixedUpdate(Transform trans) {
      if (_actFlag) {
        trans.Translate(_direction * 0.04f);
        _actFlag = false;
      }
    }

    public void MoveUp() {
      Move(Vector2.up);
    }

    public void MoveDown() {
      Move(Vector2.down);
    }

    private void Move(Vector2 direction) {
      _actFlag = true;
      _direction = direction;
    }

    private bool _actFlag;
    private Vector3 _direction;
  }
}

