using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
  void Update() {
    InputMove();
    InputJump();
    InputClimb();
  }

  void FixedUpdate() {
    Move();
    Jump();
    Climb();
    Debug.Log(_isAir);
  }

  private void InputMove() {
    _inputVec.x = Input.GetKey(KeyCode.RightArrow) ? 1 :
                  Input.GetKey(KeyCode.LeftArrow) ? -1 :
                                                     0;
  }

  private void InputJump() {
    if (!_jumpFlag)
      _jumpFlag = Input.GetButtonDown("Jump");
  }

  private void InputClimb() {

  }

  private void InputLieDown() {

  }

  private void Move() {
    if (_inputVec.x != 0) {
      if (_isAir) {

      } else {
        if (Math.Abs(_rigid.velocity.x) > _speedLimit)
          _rigid.velocity = _inputVec * _speedLimit + new Vector2(0, _rigid.velocity.y);
        else
          _rigid.AddForce(_inputVec * _forceMove);
      }
    }
  }

  private void Jump() {
    if (_jumpFlag) {
      _rigid.AddForce(Vector2.up * _forceJump);
      _isAir = true;
      _jumpFlag = false;
    }

    if (_isAir) {
      _isAir = !Physics2D.Linecast(_trans.position - Vector3.up * 0.3f,
                                   _trans.position - Vector3.up * 0.5f, layerGround);
    }
    // Debug.DrawLine(_trans.position - Vector3.up * 0.3f, _trans.position - Vector3.up * 0.5f);
  }

  private void Climb() {

  }

  private void LieDown() {

  }

  [SerializeField] private float _forceMove;
  [SerializeField] private float _forceJump;
  [SerializeField] private float _speedLimit;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private Transform _trans;
  [SerializeField] private LayerMask layerGround;
  private bool _jumpFlag;
  private bool _isAir;
  private bool _isLadder;
  private Vector2 _inputVec;
}
