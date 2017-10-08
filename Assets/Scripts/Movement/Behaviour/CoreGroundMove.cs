using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreGroundMove : GroundMove {
    public void FixedUpdate(Rigidbody2D rigid, PlayerCore core) {
      if (_actFlag) {
        rigid.velocity = new Vector2(0, 0); // like Aizen

        float ratio = (float)((core.Speed + 100) / 100.0);
        rigid.AddForce(_direction * _force * ratio);

        _actFlag = false;
        _direction.x = 0;
      }
    }
  }
}

