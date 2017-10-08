using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LobbyPlayerMovement {
    public LobbyPlayerMovement() {
      _airMove      = new AirMove();
      _groundMove   = new GroundMove();
      _ladderMove   = new LadderMove();

      _groundJump   = new GroundJump();
      _ladderJump   = new LadderJump();
      _stepDownJump = new StepDownJump();

      _lieDown      = new LieDown();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid, Transform trans) {
      _airMove.FixedUpdate(rigid);
      _groundMove.FixedUpdate(rigid);
      _ladderMove.FixedUpdate(trans);

      _groundJump.FixedUpdate(rigid);
      _ladderJump.FixedUpdate(rigid);
      _stepDownJump.FixedUpdate(rigid);
    }

    public void AirMoveLeft() {
      _airMove.MoveLeft();
    }

    public void AirMoveRight() {
      _airMove.MoveRight();
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _groundMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _groundMove.MoveRight(degAngle);
    }

    public void LadderMoveUp() {
      _ladderMove.MoveUp();
    }

    public void LadderMoveDown() {
      _ladderMove.MoveDown();
    }

    public void GroundJump() {
      _groundJump.Jump();
    }

    public void LadderJump() {
      _ladderJump.JumpOff();
    }

    public void StepDownJump() {
      _stepDownJump.StepDown();
    }

    public void LieDown(BoxCollider2D collider) {
      _lieDown.Lie(collider);
    }

    public void Stand(BoxCollider2D collider) {
      _lieDown.Stand(collider);
    }

    public void SetMoveForce(float force) {
      _groundMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _groundJump.SetForce(force);
    }

    private AirMove _airMove;
    private GroundMove _groundMove;
    private LadderMove _ladderMove;

    private GroundJump _groundJump;
    private LadderJump _ladderJump;
    private StepDownJump _stepDownJump;

    private LieDown _lieDown;
  }
}

