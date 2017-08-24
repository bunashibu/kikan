using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyClimbSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player == null)
      _player = animator.GetComponent<LobbyPlayer>();

    _player.Rigid.isKinematic = true;
    _player.Rigid.velocity = new Vector2(0.0f, 0.0f);
    _player.FootCollider.isTrigger = true;

    _isTransferable = false;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      Climb();

      if (_player.State.Ladder)
        _isTransferable = true;

      if (_isTransferable) {
        if ( ShouldTransitToClimbJump() ) { _player.StateTransfer.TransitTo ( "ClimbJump" , animator ) ; return; }
        if ( ShouldTransitToIdle()      ) { _player.StateTransfer.TransitTo ( "Idle"      , animator ) ; return; }
        if ( ShouldTransitToFall()      ) { _player.StateTransfer.TransitTo ( "Fall"      , animator ) ; return; }
      }
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _player.Rigid.isKinematic = false;
    _player.FootCollider.isTrigger = false;
  }

  private void Climb() {
    bool OnlyUpKeyDown = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
    if (OnlyUpKeyDown)
      _player.Movement.ClimbUp();

    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    if (OnlyDownKeyDown)
      _player.Movement.ClimbDown();
  }

  private bool ShouldTransitToClimbJump() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    return (OnlyLeftKeyDown || OnlyRightKeyDown) && Input.GetButton("Jump");
  }

  private bool ShouldTransitToIdle() {
    return _player.State.LadderBottomEdge && _player.State.Ground;
  }

  private bool ShouldTransitToFall() {
    return _player.State.Air && !_player.State.Ladder;
  }

  private LobbyPlayer _player;
  private bool _isTransferable;
}

