using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundLinearMove))]
[RequireComponent(typeof(GroundJump))]
[RequireComponent(typeof(AirLinearMove))]
[RequireComponent(typeof(Climb))]
[RequireComponent(typeof(StepDownJump))]
[RequireComponent(typeof(LieDown))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(RigidState))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerManipulator : MonoBehaviour {
  void Update() {
    GroundLinearMoveUpdate();
    GroundJumpUpdate();
    AirLinearMoveUpdate();
    ClimbUpdate();
    StepDownJumpUpdate();
    LieDownUpdate();
  }

  private void GroundLinearMoveUpdate() {
    if (_state.Ground) {
      if (Input.GetKey(KeyCode.LeftArrow)) {
        _groundLinear.MoveLeft();
        _renderer.flipX = false;
      }

      if (Input.GetKey(KeyCode.RightArrow)) {
        _groundLinear.MoveRight();
        _renderer.flipX = true;
      }
    }
  }

  private void GroundJumpUpdate() {
    if (_state.Ground && _state.Stand) {
      if (Input.GetButton("Jump"))
        _groundJump.Jump();
    }
  }

  private void AirLinearMoveUpdate() {
    if (_state.Air) {
      if (Input.GetKey(KeyCode.LeftArrow)) {
        _airLinear.MoveLeft();
        _renderer.flipX = false;
      }

      if (Input.GetKey(KeyCode.RightArrow)) {
        _airLinear.MoveRight();
        _renderer.flipX = true;
      }
    }
  }

  private void ClimbUpdate() {
    if (_state.Ladder) {
      if (Input.GetKey(KeyCode.UpArrow))
        _climb.MoveUp();

      if (Input.GetKey(KeyCode.DownArrow))
        _climb.MoveDown();
    }
  }

  private void StepDownJumpUpdate() {
    if (_state.LieDown) {
      if (Input.GetButton("Jump"))
        _stepDown.StepDown(_colliderFoot);
    }
  }

  private void LieDownUpdate() {
    if (_state.Stand || _state.LieDown) {
      if (Input.GetKey(KeyCode.DownArrow))
        _lieDown.Lie();

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        _lieDown.Stay();
    }
  }

  private void FlipSprite() {
    _renderer.flipX = !_renderer.flipX;
  }

  [SerializeField] private GroundLinearMove _groundLinear;
  [SerializeField] private GroundJump _groundJump;
  [SerializeField] private AirLinearMove _airLinear;
  [SerializeField] private Climb _climb;
  [SerializeField] private StepDownJump _stepDown;
  [SerializeField] private LieDown _lieDown;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _colliderFoot;
  [SerializeField] private RigidState _state;
  [SerializeField] private SpriteRenderer _renderer;
}

