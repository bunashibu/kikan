using UnityEngine;
using System.Collections;

public class JumpSystem : MovementSystem {
  void FixedUpdate() {
    _rigid.AddForce(_inputVec);
  }

  public void Jump(float force) {
    _inputVec.y = force;
  }

  public void Stay() {
    _inputVec.y = 0;
  }
}

