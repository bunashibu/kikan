using UnityEngine;
using System.Collections;

public class ClimbJump : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
      _rigid.AddForce(Vector2.up * 300.0f);
      _actFlag = false;
    }
  }

  public void JumpOff() {
    _actFlag = true;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private bool _actFlag;
}

