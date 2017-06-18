using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerMovement {
  public void AirMoveLeft() {
    _airLinear.MoveLeft();
  }

  public void AirMoveRight() {
    _airLinear.MoveRight();
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

  public void LieDown() {
    _lieDown.Lie();
  }

  public void Stand() {
    _lieDown.Stand();
  }

  private AirLinearMove _airLinear;
  private GroundLinearMove _groundLinear;

  private GroundJump _groundJump;
  private ClimbJump _climbJump;
  private StepDownJump _stepDownJump;

  private Climb _climb;
  private LieDown _lieDown;
}

