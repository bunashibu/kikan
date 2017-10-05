using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleGroundJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();

      _player.Movement.GroundJump();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        AirMove();

        if ( _player.Hp.Cur <= 0    ) { _player.StateTransfer.TransitTo( "Die"   , animator ); return; }
        if ( ShouldTransitToSkill() ) { _player.StateTransfer.TransitTo( "Skill" , animator ); return; }
        if ( ShouldTransitToFall()  ) { _player.StateTransfer.TransitTo( "Fall"  , animator ); return; }
        if ( ShouldTransitToIdle()  ) { _player.StateTransfer.TransitTo( "Idle"  , animator ); return; }
      }
    }

    private void AirMove() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.AirMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.AirMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private bool ShouldTransitToSkill() {
      bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using );

      return SkillFlag;
    }

    private bool ShouldTransitToFall() {
      return _player.State.Air && (_player.Rigid.velocity.y < 0);
    }

    private bool ShouldTransitToIdle() {
      return _player.State.Ground && Mathf.Approximately(_player.Rigid.velocity.y, 0);
    }

    private BattlePlayer _player;
  }
}

