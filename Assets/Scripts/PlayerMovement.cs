using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
  void Update() {
    UpdateState();
    InputMove();
    InputJump();
    InputClimb();
    InputLieDown();
  }

  void FixedUpdate() {
    Move();
    Jump();
    Climb();
    LieDown();
    //Debug.Log(_isAir);
  }

  private void UpdateState() {
    _isAir = !Physics2D.Linecast(_trans.position - Vector3.up * 0.48f,
                                 _trans.position - Vector3.up * 0.54f, _layerGround);
    //Debug.DrawLine(_trans.position - Vector3.up * 0.48f, _trans.position - Vector3.up * 0.54f);
  }

  private void InputMove() {
    _inputVec.x = Input.GetKey(KeyCode.RightArrow) ? 1 :
                  Input.GetKey(KeyCode.LeftArrow) ? -1 :
                                                     0;
  }

  private void InputJump() {
    if (!_jumpFlag && !_isAir && !_isLying)
      _jumpFlag = Input.GetButtonDown("Jump");
  }

  private void InputClimb() {

  }

  private void InputLieDown() {
    _isLying = _isAir                          ? false :
               Input.GetKey(KeyCode.DownArrow) ? true  :
                                                 false;
    if (_isLying) {
      //_trans.pivot = new Vector2(0.5f, 0.3f);
      _collider.size = new Vector2(0.9f, 0.6f);
      _collider.offset = new Vector2(0.0f, -0.2f);
    } else {
      _trans.pivot = new Vector2(0.5f, 0.5f);
      _collider.size = new Vector2(0.8f, 1.0f);
      _collider.offset = new Vector2(0.0f, 0.0f);
    }

    _anim.SetBool("LieDown", _isLying);
  }

  private void Move() {
    if (_inputVec.x != 0 && !_isLadder && !_isLying) {
      if (_isAir)
          _rigid.AddForce(_inputVec * _forceMove * 0.02f);
      else {
        if (Math.Abs(_rigid.velocity.x) > _speedLimitMove)
          _rigid.velocity = _inputVec * _speedLimitMove + new Vector2(0, _rigid.velocity.y);
        else
          _rigid.AddForce(_inputVec * _forceMove);
      }
    }

    if (Math.Abs(_rigid.velocity.y) > _speedLimitFall)
      _rigid.velocity = -1.0f * Vector2.up * _speedLimitFall + new Vector2(_rigid.velocity.x, 0);
  }

  private void Jump() {
    if (_jumpFlag) {
      _rigid.AddForce(Vector2.up * _forceJump);
      _jumpFlag = false;
    }
  }

  private void Climb() {

  }

  private void LieDown() {

  }

  [SerializeField] private float _forceMove;
  [SerializeField] private float _forceJump;
  [SerializeField] private float _speedLimitMove;
  [SerializeField] private float _speedLimitFall;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private RectTransform _trans;
  [SerializeField] private LayerMask _layerGround;
  [SerializeField] private Animator _anim;
  private bool _jumpFlag;
  private bool _isAir;
  private bool _isLadder;
  private bool _isLying;
  private Vector2 _inputVec;
}
