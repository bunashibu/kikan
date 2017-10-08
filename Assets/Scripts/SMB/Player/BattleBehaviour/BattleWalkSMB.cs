using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleWalkSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      Debug.Log("Walk");

      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        GroundMove();

        if ( _player.Hp.Cur <= 0           ) { _player.StateTransfer.TransitTo( "Die"          , animator ); return; }
        if ( ShouldTransitToSkill()        ) { _player.StateTransfer.TransitTo( "Skill"        , animator ); return; }
        if ( ShouldTransitToClimb()        ) { _player.StateTransfer.TransitTo( "Climb"        , animator ); return; }
        if ( ShouldTransitToStepDownJump() ) { _player.StateTransfer.TransitTo( "StepDownJump" , animator ); return; }
        if ( ShouldTransitToGroundJump()   ) { _player.StateTransfer.TransitTo( "GroundJump"   , animator ); return; }
        if ( ShouldTransitToIdle()         ) { _player.StateTransfer.TransitTo( "Idle"         , animator ); return; }
        if ( ShouldTransitToFall()         ) { _player.StateTransfer.TransitTo( "Fall"         , animator ); return; }
      }
    }

    private void GroundMove() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

      if (OnlyLeftKeyDown)  {
        float degAngle = _player.State.GroundAngle;
        degAngle *= _player.State.GroundLeft ? 1 : -1;

        _player.Movement.GroundMoveLeft(degAngle);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      if (OnlyRightKeyDown) {
        float degAngle = _player.State.GroundAngle;
        degAngle *= _player.State.GroundRight ? 1 : -1;

        _player.Movement.GroundMoveRight(degAngle);

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

    private bool ShouldTransitToClimb() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool ClimbFlag       = ( OnlyUpKeyDown   && !_player.State.LadderTopEdge    ) ||
                             ( OnlyDownKeyDown && !_player.State.LadderBottomEdge );

      return _player.State.Ladder && ClimbFlag;
    }

    private bool ShouldTransitToStepDownJump() {
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);

      return !_player.State.CanNotDownGround && _player.State.Ground && OnlyDownKeyDown && Input.GetButton("Jump");
    }

    private bool ShouldTransitToGroundJump() {
      return _player.State.Ground && Mathf.Approximately(_player.Rigid.velocity.y, 0) && Input.GetButton("Jump");
    }

    private bool ShouldTransitToIdle() {
      bool BothKeyDown = Input.GetKey(KeyCode.LeftArrow)   && Input.GetKey(KeyCode.RightArrow);
      bool OneKeyUp    = Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);

      return _player.State.Ground && (BothKeyDown || OneKeyUp);
    }

    private bool ShouldTransitToFall() {
      return _player.State.Air && (_player.Rigid.velocity.y < 0);
    }

    private BattlePlayer _player;
  }
}

