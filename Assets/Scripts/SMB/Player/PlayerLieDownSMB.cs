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
        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"         ); return; }
        if ( _player.State.Rigor           )         { SyncAnimation( "Skill"        ); return; }
        if ( ShouldTransitToWalk()         )         { SyncAnimation( "Walk"         ); return; }
        if ( ShouldTransitToStepDownJump() )         { SyncAnimation( "StepDownJump" ); return; }
        if ( ShouldTransitToIdle()         )         { SyncAnimation( "Idle"         ); return; }
        if ( _player.Location.IsAir        )         { SyncAnimation( "Fall"         ); return; }
      }
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
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
      return _player.Location.IsGround && !Input.GetKey(KeyCode.DownArrow);
    }

    private Player _player;
    private Vector2 _prevOffset;
    private Vector2 _prevSize;
  }
}
