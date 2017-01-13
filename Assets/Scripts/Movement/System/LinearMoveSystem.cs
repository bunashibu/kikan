using UnityEngine;
using System.Collections;

public class LinearMoveSystem : MonoBehaviour {
  void FixedUpdate() {
    if (System.Math.Abs(_rigid.velocity.x) <= _limit)
      _rigid.AddForce(_inputVec * _force);
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

  public void SetForce(float force) {
    _force = force;
  }

  public void SetLimit(float limit) {
    _limit = limit;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private float _force;
  private float _limit;
  private Vector2 _inputVec;
}

