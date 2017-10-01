using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LinearJumpEnemyMovement {
    public LinearJumpEnemyMovement() {
      _groundLinear = new GroundLinearMove();
      _groundJump   = new GroundJump();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid) {
      //_groundLinear.FixedUpdate(rigid);
      _groundJump.FixedUpdate(rigid);
    }

    public void GroundMoveLeft() {
      _groundLinear.MoveLeft();
    }

    public void GroundMoveRight() {
      _groundLinear.MoveRight();
    }

    public void GroundJump() {
      _groundJump.Jump();
    }

    public void SetLinearMoveForce(float force) {
      _groundLinear.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _groundJump.SetForce(force);
    }

    private GroundLinearMove _groundLinear;
    private GroundJump _groundJump;
  }
}

