using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerIdleSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Debuff.State[DebuffType.Stun] ) { _player.StateTransfer.TransitTo( "Stun"       , animator ); return; }
        if ( _player.State.Rigor                   ) { _player.StateTransfer.TransitTo( "Skill"      , animator ); return; }
        if ( ShouldTransitToLadder()               ) { _player.StateTransfer.TransitTo( "Ladder"     , animator ); return; }
        if ( ShouldTransitToWalk()                 ) { _player.StateTransfer.TransitTo( "Walk"       , animator ); return; }
        if ( ShouldTransitToLieDown()              ) { _player.StateTransfer.TransitTo( "LieDown"    , animator ); return; }
        if ( ShouldTransitToGroundJump()           ) { _player.StateTransfer.TransitTo( "GroundJump" , animator ); return; }
        if ( _player.Location.IsAir                ) { _player.StateTransfer.TransitTo( "Fall"       , animator ); return; }
      }
    }

    private bool ShouldTransitToLadder() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LadderFlag      = ( OnlyUpKeyDown   && !_player.Location.IsLadderTopEdge) ||
                             ( OnlyDownKeyDown && !_player.Location.IsLadderBottomEdge);

      return _player.Location.IsLadder && LadderFlag;
    }

    private bool ShouldTransitToWalk() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

      return _player.Location.IsGround && WalkFlag;
    }

    private bool ShouldTransitToLieDown() {
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LieDownFlag     = OnlyDownKeyDown && (!_player.Location.IsLadder || (_player.Location.IsLadderBottomEdge && _player.Location.IsGround));

      return _player.Location.IsGround && LieDownFlag;
    }

    private bool ShouldTransitToGroundJump() {
      return _player.Location.IsGround && Input.GetButton("Jump") && !_player.State.Heavy;
    }

    private Player _player;
  }
}

