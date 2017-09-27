using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
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
  
    protected bool _actFlag;
    protected Vector2 _inputVec;
  }
}

