using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreFlatGroundMove : FlatGroundMove {
    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        rigid.velocity = new Vector2(0, rigid.velocity.y); // like Aizen

        float ratio = (float)((core.Speed + 100) / 100.0);
        rigid.AddForce(_inputVec * _force * ratio);

        _actFlag = false;
        _inputVec.x = 0;
      }
    }
  }
}

