using UnityEngine;
using System.Collections;

public class KinokoManipulator : MonoBehaviour {
  void Update() {
    GroundLinearMoveUpdate();
    GroundJumpUpdate();
  }

  private void GroundLinearMoveUpdate() {
    if (_state.Ground) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _groundLinear.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _groundLinear.MoveRight();
    }
  }

  private void GroundJumpUpdate() {
    if (_state.Ground && _state.Stand) {
      if (Input.GetButton("Jump"))
        _groundJump.Jump();
    }
  }

  [SerializeField] private GroundLinearMove _groundLinear;
  [SerializeField] private GroundJump _groundJump;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private RigidState _state;
}

