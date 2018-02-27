using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyMovement {
    public EnemyMovement () {
      _groundMove   = new GroundMove();
      _groundJump   = new GroundJump();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid) {
      _groundMove.FixedUpdate(rigid);
      _groundJump.FixedUpdate(rigid);
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _groundMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _groundMove.MoveRight(degAngle);
    }

    public void GroundJump() {
      _groundJump.Jump();
    }

    public void SetMoveForce(float force) {
      _groundMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _groundJump.SetForce(force);
    }

    private GroundMove _groundMove;
    private GroundJump _groundJump;
  }
}

