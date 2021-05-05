using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerWalkSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        GroundMove();

        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"         ); return; }
        if ( _player.State.Rigor           )         { SyncAnimation( "Skill"        ); return; }
        if ( ShouldTransitToLadder()       )         { SyncAnimation( "Ladder"       ); return; }
        if ( ShouldTransitToStepDownJump() )         { SyncAnimation( "StepDownJump" ); return; }
        if ( ShouldTransitToGroundJump()   )         { SyncAnimation( "GroundJump"   ); return; }
        if ( ShouldTransitToIdle()         )         { SyncAnimation( "Idle"         ); return; }
        if ( ShouldTransitToFall()         )         { SyncAnimation( "Fall"         ); return; }
      }
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
    }

    private void GroundMove() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

      if (OnlyLeftKeyDown)  {
        float angle;

        if (_player.Location.IsLeftSlope)
          angle = _player.Location.SlopeAngle;
        else if (_player.Location.IsRightSlope)
          angle = _player.Location.GroundAngle * -1;
        else if (_player.Location.IsRightGround)
          angle = _player.Location.GroundAngle * -1;
        else
          angle = _player.Location.GroundAngle;

        _player.Movement.GroundMoveLeft(angle);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      if (OnlyRightKeyDown) {
        float angle;

        if (_player.Location.IsRightSlope)
          angle = _player.Location.SlopeAngle;
        else if (_player.Location.IsLeftSlope)
          angle = _player.Location.GroundAngle * -1;
        else if (_player.Location.IsLeftGround)
          angle = _player.Location.GroundAngle * -1;
        else
          angle = _player.Location.GroundAngle;

        _player.Movement.GroundMoveRight(angle);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private bool ShouldTransitToLadder() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);

      return (OnlyUpKeyDown && _player.Location.GrabBottomLadder) || (OnlyDownKeyDown && _player.Location.GrabTopLadder);
    }

    private bool ShouldTransitToStepDownJump() {
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);

      return !_player.Location.IsCanNotDownGround && _player.Location.IsGround && OnlyDownKeyDown && Input.GetButton("Jump") && !_player.State.Heavy;
    }

    private bool ShouldTransitToGroundJump() {
      return _player.Location.IsGround && Input.GetButton("Jump") && !_player.State.Heavy;
    }

    private bool ShouldTransitToIdle() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool IdleFlag = !OnlyLeftKeyDown && !OnlyRightKeyDown;

      return _player.Location.IsGround && IdleFlag;
    }

    private bool ShouldTransitToFall() {
      return _player.Location.IsAir && (_player.Rigid.velocity.y < 0);
    }

    private Player _player;
  }
}
