using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreAirLinearMove : AirLinearMove {
    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        float ratio = (float)((core.Speed + 100) / 100.0);
        rigid.AddForce(_inputVec * 2.0f * ratio);

        _actFlag = false;
        _inputVec.x = 0;
      }
    }
  }
}

