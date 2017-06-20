using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySkillSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _transitionFlag = false;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      if (_player.RigidState.Rigor)
        _transitionFlag = true;

      if (_transitionFlag && !_player.RigidState.Rigor) {
        if ( ShouldTransitToWalk()     ) { _player.StateTransfer.TransitTo( "Walk" , animator ); return; }
        if ( _player.RigidState.Ground ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
        if ( _player.RigidState.Air    ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
      }
    }
  }

  private bool ShouldTransitToWalk() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

    return _player.RigidState.Ground && WalkFlag;
  }

  [SerializeField] private LobbyPlayerSMB _player;
  private bool _transitionFlag;
}

