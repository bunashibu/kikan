using UnityEngine;
using System.Collections;

public class StepDownJump : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
      _rigid.AddForce(Vector2.up * 200.0f);
      _actFlag = false;
    }
  }

  public void StepDown() {
    _actFlag = true;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private bool _actFlag;
}

