using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {
  void Update() {
    UpdateState();
    /*
    if (_isLying && _jumpFlag)
      _colliderGround.isTrigger = true;
    */

    //_anim.SetBool("Jump", _isAir);
    //_anim.SetBool("Walk", _moveFlag);
    _anim.SetBool("LieDown", _isLying);
  }

  void FixedUpdate() {
    CheckSpeedLimit();
  }

  void OnTriggerExit2D() {
    _colliderGround.isTrigger = false;
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

  [SerializeField] private float _speedLimitHorizontal;
  [SerializeField] private float _speedLimitVertical;
  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private BoxCollider2D _colliderGround;
  [SerializeField] private RectTransform _trans;
  [SerializeField] private LayerMask _layerGround;
  [SerializeField] private Animator _anim;
  private bool _isAir;
  private bool _isLying;
}

