using UnityEngine;
using System.Collections;

public class LinearMoveSystem : MovementSystem {
  void FixedUpdate() {
    if (System.Math.Abs(_rigid.velocity.x) <= _limit)
      _rigid.AddForce(_inputVec);

  }

  public void MoveLeft(float force) {
    _inputVec.x -= force;

    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight(float force) {
    _inputVec.x += force;

    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void Stay() {
    _inputVec.x = 0;
  }

  public float _limit;
}

