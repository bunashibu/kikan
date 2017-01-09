using UnityEngine;
using System.Collections;

public class JumpSystem : MovementSystem {
  void FixedUpdate() {
    _rigid.AddForce(_inputVec);
  }

  public void Jump() {
    _inputVec.y = 1;
  }

  public void Stay() {
    _inputVec.y = 0;
  }
}

