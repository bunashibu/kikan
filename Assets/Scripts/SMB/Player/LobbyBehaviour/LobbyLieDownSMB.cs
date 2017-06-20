using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLieDownSMB : StateMachineBehaviour {
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      if ( ShouldTransitToWalk()         ) { _player.StateTransfer.TransitTo( "Walk"         , animator ); return; }
      if ( ShouldTransitToStepDownJump() ) { _player.StateTransfer.TransitTo( "StepDownJump" , animator ); return; }
      if ( ShouldTransitToIdle()         ) { _player.StateTransfer.TransitTo( "Idle"         , animator ); return; }
      if ( _player.RigidState.Air        ) { _player.StateTransfer.TransitTo( "Fall"         , animator ); return; }
    }
  }

  private bool ShouldTransitToWalk() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

    return _player.RigidState.Ground && WalkFlag;
  }

  private bool ShouldTransitToStepDownJump() {
    return _player.RigidState.Ground && Input.GetButton("Jump");
  }

  private bool ShouldTransitToIdle() {
    bool DownKeyUp = Input.GetKeyUp(KeyCode.DownArrow);
    bool UpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);

    return _player.RigidState.Ground && (DownKeyUp || UpKeyDown);
  }

  [SerializeField] private LobbyPlayerSMB _player;
}

