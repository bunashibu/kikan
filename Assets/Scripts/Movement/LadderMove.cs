using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LadderMove {
    public void FixedUpdate(Transform trans) {
      if (_actFlag) {
        trans.Translate(_direction * 0.04f * _ratio);
        _actFlag = false;
      }
    }

    public void FixedUpdate(Transform trans, Core core) {
      if (_actFlag) {
        float coreRatio = (float)((core.GetValue(CoreType.Speed) + 100) / 100.0);
        trans.Translate(_direction * 0.04f * _ratio * coreRatio);
        _actFlag = false;
      }
    }

    public void SetRatio(float ratio) {
      _ratio = ratio;
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
    private float _ratio = 1.0f;
  }
}

