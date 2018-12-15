using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerGroundJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _isAlreadyJumped = false;
      _player.Movement.GroundJump();
      _player.AudioEnvironment.PlayOneShot("Jump");
      ApplyInertia();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if (_player.Location.IsAir)
          _isAlreadyJumped = true;

        AirMove();

        //if ( _player.BuffState.Stun  ) { _player.StateTransfer.TransitTo( "Stun"   , animator ); return; }
        //if ( ShouldTransitToSkill()  ) { _player.StateTransfer.TransitTo( "Skill"  , animator ); return; }
        if ( ShouldTransitToLadder() ) { _player.StateTransfer.TransitTo( "Ladder" , animator ); return; }
        if ( ShouldTransitToFall()   ) { _player.StateTransfer.TransitTo( "Fall"   , animator ); return; }
      }
    }

    private void ApplyInertia() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown && _player.Renderers[0].flipX == false)
        _player.Movement.GroundMoveLeft(0);

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown && _player.Renderers[0].flipX == true)
        _player.Movement.GroundMoveRight(0);
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
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Ctrl  ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Space ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Alt   ) == SkillState.Using );

      return SkillFlag;
    }

    private bool ShouldTransitToLadder() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LadderFlag      = ( OnlyUpKeyDown   && !_player.Location.IsLadderTopEdge) ||
                             ( OnlyDownKeyDown && !_player.Location.IsLadderBottomEdge);

      return _player.Location.IsLadder && LadderFlag;
    }

    private bool ShouldTransitToFall() {
      if (_isAlreadyJumped)
        return _player.Rigid.velocity.y <= 0;

      return false;
    }

    private Player _player;
    private bool _isAlreadyJumped;
  }
}

