using UnityEngine;
using System.Collections;

public class LinearMoveSystem : MovementSystem {
  void FixedUpdate() {
    if (System.Math.Abs(_rigid.velocity.x) <= _limit)
      _rigid.AddForce(_inputVec);
  }

  public void MoveLeft() {
    _inputVec.x -= 1;

    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight() {
    _inputVec.x += 1;

    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void Stay() {
    _inputVec.x = 0;
  }

  [SerializeField] private float _limit;
}

