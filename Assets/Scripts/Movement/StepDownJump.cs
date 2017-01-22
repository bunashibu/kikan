using UnityEngine;
using System.Collections;

public class StepDownJump : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.AddForce(Vector2.up * _force);
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

  [SerializeField] private Rigidbody2D _rigid;
  private float _force;
  private bool _actFlag;
}

