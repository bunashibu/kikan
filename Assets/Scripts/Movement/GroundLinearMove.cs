using UnityEngine;
using System.Collections;

public class GroundLinearMove : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      if (System.Math.Abs(_rigid.velocity.x) <= _limit)
        _rigid.AddForce(_inputVec * _force);

      _actFlag = false;
    }
  }

  public void MoveLeft(Animator anim, string name) {
    anim.SetTrigger(name);
    _actFlag = true;

    _inputVec.x -= 1;
    if (_inputVec.x < -1)
      _inputVec.x = -1;
  }

  public void MoveRight(Animator anim, string name) {
    anim.SetTrigger(name);
    _actFlag = true;

    _inputVec.x += 1;
    if (_inputVec.x > 1)
      _inputVec.x = 1;
  }

  public void SetForce(float force) {
    _force = force;
  }

  public void SetLimit(float limit) {
    _limit = limit;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private float _force;
  private float _limit;
  private bool _actFlag;
  private Vector2 _inputVec;
}

