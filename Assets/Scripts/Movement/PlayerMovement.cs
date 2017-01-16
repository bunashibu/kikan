using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
  void Update() {
    GroundLinearMoveUpdate();
    GroundJumpUpdate();
    AirLinearMoveUpdate();
    ClimbUpdate();
    StepDownJumpUpdate();
    LieDownUpdate();
  }

  void FixedUpdate() {
    _groundLinearSys.CallFixedUpdate(_rigid);
    _groundJumpSys.CallFixedUpdate(_rigid);
    _airLinearSys.CallFixedUpdate(_rigid);
    _climbSys.CallFixedUpdate(_rigid);
    _stepDownSys.CallFixedUpdate(_rigid);
  }

  private void GroundLinearMoveUpdate() {
    if (_state.Ground) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _groundLinearSys.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _groundLinearSys.MoveRight();
    }
  }

  private void GroundJumpUpdate() {
    if (_state.Ground && _state.Stand) {
      if (Input.GetButton("Jump"))
        _groundJumpSys.Jump();
    }
  }

  private void AirLinearMoveUpdate() {
    if (_state.Air) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _airLinearSys.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _airLinearSys.MoveRight();
    }
  }

  private void ClimbUpdate() {
    if (_state.Ladder) {
      if (Input.GetKey(KeyCode.UpArrow))
        _climbSys.MoveUp();

      if (Input.GetKey(KeyCode.DownArrow))
        _climbSys.MoveDown();
    }
  }

  private void StepDownJumpUpdate() {
    if (_state.LieDown) {
      if (Input.GetButton("Jump"))
        _stepDownSys.StepDown(_colliderFoot);
    }
  }

  private void LieDownUpdate() {
    if (_state.Stand || _state.LieDown) {
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
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _colliderFoot;
  [SerializeField] private RigidState _state;
}

