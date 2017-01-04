using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
  void Update() {
    UpdateState();
    InputMove();
    InputJump();
    LieDown();

    _anim.SetBool("Walk", _moveFlag);
    _anim.SetBool("Jump", _isAir);
    _isLadder = false;
  }

  void FixedUpdate() {
    Move();
    Jump();
    CheckSpeedLimit();
  }

  void OnCollisionStay2D() {
    if (_rigid.velocity.y <= 0)
      _isAir = false;
  }

  void OnCollisionExit2D() {
    _isAir = true;
  }

  private void UpdateState() {
    _isLying = _isAir                          ? false :
               Input.GetKey(KeyCode.DownArrow) ? true  :
                                                 false;

    canMove = !_isLadder &&
               !_isLying;

    canJump = !_jumpFlag &&
               !_isAir    &&
               !_isLying;
  }

  private void InputMove() {
    _inputVec.x = Input.GetKey(KeyCode.RightArrow) ? 1 :
                  Input.GetKey(KeyCode.LeftArrow) ? -1 :
                                                     0;
    if (canMove) {
      _moveFlag = (_inputVec.x != 0) ? true :
                                       false;
    }
  }

  private void InputJump() {
    if (canJump)
      _jumpFlag = Input.GetButtonDown("Jump");
  }

  private void LieDown() {
    if (_isLying) {
      _collider.size = new Vector2(0.9f, 0.6f);
      _collider.offset = new Vector2(0.0f, -0.2f);
    } else {
      _collider.size = new Vector2(0.8f, 1.0f);
      _collider.offset = new Vector2(0.0f, 0.0f);
    }

    _anim.SetBool("LieDown", _isLying);
  }

  private void Move() {
    if (!_moveFlag) return;

    if (Math.Abs(_rigid.velocity.x) <= _speedLimitMove) {
      if (_isAir)
        _rigid.AddForce(_inputVec * _forceMove * 0.02f);
      else
        _rigid.AddForce(_inputVec * _forceMove);
    }
  }

  private void Jump() {
    if (!_jumpFlag) return;

    _rigid.AddForce(Vector2.up * _forceJump);
    _jumpFlag = false;
  }

  private void CheckSpeedLimit() {
    /*
    if (Math.Abs(_rigid.velocity.x) > _speedLimitHorizontal)
      _rigid.velocity = Vector2.right * _speedLimitHorizontal + new Vector2(0, _rigid.velocity.y);
    */

    // Down
    if (Math.Abs(_rigid.velocity.y) > _speedLimitVertical)
      _rigid.velocity = Vector2.down * _speedLimitVertical + new Vector2(_rigid.velocity.x, 0);
  }

  [SerializeField] private float _forceMove;
  [SerializeField] private float _forceJump;
  [SerializeField] private float _speedLimitMove;
  [SerializeField] private float _speedLimitHorizontal;
  [SerializeField] private float _speedLimitVertical;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private RectTransform _trans;
  [SerializeField] private LayerMask _layerGround;
  [SerializeField] private Animator _anim;
  public bool canMove { get; private set; }
  public bool canJump { get; private set; }
  private bool _moveFlag;
  private bool _jumpFlag;
  private bool _isAir;
  private bool _isLadder;
  private bool _isLying;
  private Vector2 _inputVec;
}
