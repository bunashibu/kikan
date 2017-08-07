using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearEnemyMovement {
  public LinearEnemyMovement() {
    _groundLinear = new GroundLinearMove();
  }

  // INFO: Must be called in MonoBehaviour-FixedUpdate()
  public void FixedUpdate(Rigidbody2D rigid) {
    _groundLinear.FixedUpdate(rigid);
  }

  public void GroundMoveLeft() {
    _groundLinear.MoveLeft();
  }

  public void GroundMoveRight() {
    _groundLinear.MoveRight();
  }

  public void SetLinearMoveForce(float force) {
    _groundLinear.SetForce(force);
  }

  private GroundLinearMove _groundLinear;
}

