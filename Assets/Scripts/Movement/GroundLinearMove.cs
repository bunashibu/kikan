using UnityEngine;
using System.Collections;

public class GroundLinearMove : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.velocity = new Vector2(0, _rigid.velocity.y);
      _rigid.AddForce(_inputVec * _force);

      _actFlag = false;
      _inputVec.x = 0;
    }
  }

  public void MoveLeft() {
    _actFlag = true;

    _inputVec.x -= 1;
    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight() {
    _actFlag = true;

    _inputVec.x += 1;
    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private float _force;
  private bool _actFlag;
  private Vector2 _inputVec;
}

