using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattlePlayerMovement {
    public BattlePlayerMovement(PlayerCore core) {
      _core = core;

      _coreAirMove    = new CoreAirMove();
      _coreGroundMove = new CoreGroundMove();
      _ladderMove     = new LadderMove();

      _coreGroundJump = new CoreGroundJump();
      _coreLadderJump  = new CoreLadderJump();
      _stepDownJump   = new StepDownJump();

      _lieDown        = new LieDown();
    }

    // INFO: Must be called in MonoBehaviour-FixedUpdate()
    public void FixedUpdate(Rigidbody2D rigid, Transform trans) {
      _coreAirMove.FixedUpdate(rigid, _core);
      _coreGroundMove.FixedUpdate(rigid, _core);
      _ladderMove.FixedUpdate(trans);

      _coreGroundJump.FixedUpdate(rigid, _core);
      _coreLadderJump.FixedUpdate(rigid, _core);
      _stepDownJump.FixedUpdate(rigid);
    }

    public void AirMoveLeft() {
      _coreAirMove.MoveLeft();
    }

    public void AirMoveRight() {
      _coreAirMove.MoveRight();
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _coreGroundMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _coreGroundMove.MoveRight(degAngle);
    }

    public void LadderMoveUp() {
      _ladderMove.MoveUp();
    }

    public void LadderMoveDown() {
      _ladderMove.MoveDown();
    }

    public void GroundJump() {
      _coreGroundJump.Jump();
    }

    public void LadderJump() {
      _coreLadderJump.JumpOff();
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
      _coreGroundMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _coreGroundJump.SetForce(force);
    }

    private PlayerCore _core;

    private CoreAirMove _coreAirMove;
    private CoreGroundMove _coreGroundMove;
    private LadderMove _ladderMove;

    private CoreGroundJump _coreGroundJump;
    private CoreLadderJump _coreLadderJump;
    private StepDownJump _stepDownJump;

    private LieDown _lieDown;
  }
}

