using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GroundLinearMove))]
[RequireComponent(typeof(GroundJump))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(KinokoState))]
public class KinokoAI : MonoBehaviour {
  void Update() {
    StateUpdate();
    Behave();
  }

  private void StateUpdate() {

  }

  private void Behave() {
    _groundLinear.MoveLeft();
    _groundLinear.MoveRight();
    _groundJump.Jump();
  }

  [SerializeField] private GroundLinearMove _groundLinear;
  [SerializeField] private GroundJump _groundJump;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private KinokoState _state;
}

