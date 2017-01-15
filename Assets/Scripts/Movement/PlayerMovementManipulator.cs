using UnityEngine;
using System.Collections;

public class PlayerMovementManipulator : MonoBehaviour {
  void Update() {
    GroundLinearMoveUpdate();
    GroundJumpUpdate();
    AirLinearMoveUpdate();
    ClimbUpdate();
    StepDownJumpUpdate();
    LieDownUpdate();
  }

  private void GroundLinearMoveUpdate() {
    if (_groundLinearSys.CanUse) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _groundLinearSys.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _groundLinearSys.MoveRight();
    }
  }

  private void GroundJumpUpdate() {
    if (_groundJumpSys.CanUse) {
      if (Input.GetButton("Jump"))
        _groundJumpSys.Jump();
    }
  }

  private void AirLinearMoveUpdate() {
    if (_airLinearSys.CanUse) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _airLinearSys.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _airLinearSys.MoveRight();
    }
  }

  private void ClimbUpdate() {
    if (_climbSys.CanUse) {
      if (Input.GetKey(KeyCode.UpArrow))
        _climbSys.MoveUp();

      if (Input.GetKey(KeyCode.DownArrow))
        _climbSys.MoveDown();
    }
  }

  private void StepDownJumpUpdate() {
    if (_stepDownSys.CanUse) {
      if (Input.GetButton("Jump"))
        _stepDownSys.StepDown();
    }
  }

  private void LieDownUpdate() {
    if (_lieDownSys.CanUse) {
      if (Input.GetKey(KeyCode.DownArrow))
        _lieDownSys.LieDown();

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        _lieDownSys.Stay();
    }
  }

  [SerializeField] private GroundLinearMoveSystem _groundLinearSys;
  [SerializeField] private GroundJumpSystem _groundJumpSys;
  [SerializeField] private AirLinearMoveSystem _airLinearSys;
  [SerializeField] private ClimbSystem _climbSys;
  [SerializeField] private StepDownJumpSystem _stepDownSys;
  [SerializeField] private LieDownSystem _lieDownSys;
}

