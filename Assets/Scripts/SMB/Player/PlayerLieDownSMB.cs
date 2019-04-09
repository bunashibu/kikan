using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLieDownSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _prevOffset = _player.BodyCollider.offset;
      _prevSize = _player.BodyCollider.size;

      _player.BodyCollider.offset = new Vector2(0, -0.3f);
      _player.BodyCollider.size = new Vector2(0.5f, 0.4f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Debuff.State[DebuffType.Stun] ) { _player.StateTransfer.TransitTo( "Stun"         , animator ); return; }
        if ( _player.State.Rigor           )         { _player.StateTransfer.TransitTo( "Skill"        , animator ); return; }
        if ( ShouldTransitToWalk()         )         { _player.StateTransfer.TransitTo( "Walk"         , animator ); return; }
        if ( ShouldTransitToStepDownJump() )         { _player.StateTransfer.TransitTo( "StepDownJump" , animator ); return; }
        if ( ShouldTransitToIdle()         )         { _player.StateTransfer.TransitTo( "Idle"         , animator ); return; }
        if ( _player.Location.IsAir        )         { _player.StateTransfer.TransitTo( "Fall"         , animator ); return; }
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _player.BodyCollider.offset = _prevOffset;
      _player.BodyCollider.size = _prevSize;
    }

    private bool ShouldTransitToWalk() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

      return _player.Location.IsGround && WalkFlag;
    }

    private bool ShouldTransitToStepDownJump() {
      return !_player.Location.IsCanNotDownGround && _player.Location.IsGround && Input.GetButton("Jump") && !_player.State.Heavy;
    }

    private bool ShouldTransitToIdle() {
      bool DownKeyUp = Input.GetKeyUp(KeyCode.DownArrow);
      bool UpKeyDown = Input.GetKeyDown(KeyCode.UpArrow);

      return _player.Location.IsGround && (DownKeyUp || UpKeyDown);
    }

    private Player _player;
    private Vector2 _prevOffset;
    private Vector2 _prevSize;
  }
}

