using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerSkillSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Hp.Cur.Value <= 0 ) { _player.StateTransfer.TransitTo( "Die" , animator ); return; }
        if ( _player.BuffState.Stun    ) { _player.StateTransfer.TransitTo( "Stun", animator ); return; }

        if (!_player.State.Rigor) {
          if ( ShouldTransitToWalk()                         ) { _player.StateTransfer.TransitTo( "Walk" , animator ); return; }
          if ( LocationJudger.IsGround(_player.FootCollider) ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
          if ( LocationJudger.IsAir(_player.FootCollider)    ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
        }
      }
    }

    private bool ShouldTransitToWalk() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

      return LocationJudger.IsGround(_player.FootCollider) && WalkFlag;
    }

    private Player _player;
  }
}

