using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyClimbSMB : StateMachineBehaviour {
  /*
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView   = animator.GetComponent<PhotonView>();
      _rigid        = animator.GetComponent<Rigidbody2D>();
      _colliderFoot = animator.GetComponents<BoxCollider2D>()[1];
      _rigidState   = animator.GetComponent<RigidState>();
      _movement     = animator.GetComponent<LobbyPlayer>().Movement;

      _stateTransfer = new LobbyPlayerStateTransfer(animator);
    }

    _rigid.isKinematic = true;
    _rigid.velocity = new Vector2(0.0f, 0.0f);
    _colliderFoot.isTrigger = true;
    _isTransferable = false;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      Climb();

      if (_rigidState.Ladder)
        _isTransferable = true;

      if (_isTransferable) {
        if ( ShouldTransitToClimbJump() ) { _stateTransfer.TransitTo ( "ClimbJump" , animator ) ; return; }
        if ( ShouldTransitToIdle()      ) { _stateTransfer.TransitTo ( "Idle"      , animator ) ; return; }
        if ( ShouldTransitToFall()      ) { _stateTransfer.TransitTo ( "Fall"      , animator ) ; return; }
      }
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _rigid.isKinematic = false;
    _colliderFoot.isTrigger = false;
  }

  private void Climb() {
    bool OnlyUpKeyDown = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
    if (OnlyUpKeyDown)
      _movement.ClimbUp();

    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    if (OnlyDownKeyDown)
      _movement.ClimbDown();
  }

  private bool ShouldTransitToClimbJump() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    return (OnlyLeftKeyDown || OnlyRightKeyDown) && Input.GetButton("Jump");
  }

  private bool ShouldTransitToIdle() {
    return _rigidState.LadderBottomEdge && _rigidState.Ground;
  }

  private bool ShouldTransitToFall() {
    return _rigidState.Air && !_rigidState.Ladder;
  }

  private PhotonView _photonView;
  private Rigidbody2D _rigid;
  private BoxCollider2D _colliderFoot;
  private RigidState _rigidState;
  private LobbyPlayerMovement _movement;
  private bool _isTransferable;

  private LobbyPlayerStateTransfer _stateTransfer;
  */
}

