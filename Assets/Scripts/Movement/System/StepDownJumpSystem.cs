using UnityEngine;
using System.Collections;

public class StepDownJumpSystem : ScriptableObject {
  public void CallFixedUpdate(Rigidbody2D rigid) {
    if (_actFlag) {
      rigid.AddForce(Vector2.up * _force);
      _actFlag = false;
    }
  }

  public void StepDown(BoxCollider2D collider) {
    _actFlag = true;
    collider.isTrigger = true;
    // ImplThroughGround
  }

  public void SetForce(float force) {
    _force = force;
  }

  private float _force;
  private bool _actFlag;
}

