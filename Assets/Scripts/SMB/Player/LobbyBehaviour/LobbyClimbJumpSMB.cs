using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyClimbJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player == null)
      _player = animator.GetComponent<LobbyPlayer>();

    ClimbJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      if ( _player.State.Air ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
    }
  }

  private void ClimbJump() {
    bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
    if (OnlyLeftKeyDown) {
      _player.Movement.GroundMoveLeft();

      foreach (var sprite in _player.Renderers)
        sprite.flipX = false;
    }

    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    if (OnlyRightKeyDown) {
      _player.Movement.GroundMoveRight();

      foreach (var sprite in _player.Renderers)
        sprite.flipX = true;
    }

    _player.Movement.ClimbJump();
  }

  private LobbyPlayer _player;
}

