using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LobbyPlayerMovement {
    public LobbyPlayerMovement() {
      _airLinear    = new AirLinearMove();
      _flatMove     = new FlatGroundMove();

      _groundJump   = new GroundJump();
      _climbJump    = new ClimbJump();
      _stepDownJump = new StepDownJump();

      _climb        = new Climb();
      _lieDown      = new LieDown();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid, Transform trans) {
      _airLinear.FixedUpdate(rigid);
      _flatMove.FixedUpdate(rigid);

      _groundJump.FixedUpdate(rigid);
      _climbJump.FixedUpdate(rigid);
      _stepDownJump.FixedUpdate(rigid);

      _climb.FixedUpdate(trans);
    }

    public void AirMoveLeft() {
      _airLinear.MoveLeft();
    }

    public void AirMoveRight() {
      _airLinear.MoveRight();
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _flatMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _flatMove.MoveRight(degAngle);
    }

    public void GroundJump() {
      _groundJump.Jump();
    }

    public void ClimbJump() {
      _climbJump.JumpOff();
    }

    public void StepDownJump() {
      _stepDownJump.StepDown();
    }

    public void ClimbUp() {
      _climb.MoveUp();
    }

    public void ClimbDown() {
      _climb.MoveDown();
    }

    public void LieDown(BoxCollider2D collider) {
      _lieDown.Lie(collider);
    }

    public void Stand(BoxCollider2D collider) {
      _lieDown.Stand(collider);
    }

    public void SetLinearMoveForce(float force) {
      _flatMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _groundJump.SetForce(force);
    }

    private AirLinearMove _airLinear;
    private FlatGroundMove _flatMove;

    private GroundJump _groundJump;
    private ClimbJump _climbJump;
    private StepDownJump _stepDownJump;

    private Climb _climb;
    private LieDown _lieDown;
  }
}

