using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLadderSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _player.Rigid.isKinematic = true;
      _player.Rigid.velocity = new Vector2(0.0f, 0.0f);
      _player.FootCollider.isTrigger = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        LadderMove();

        if ( _player.Debuff.State[DebuffType.Stun] ) { _player.StateTransfer.TransitTo( "Stun",       animator ); return; }
        if ( ShouldTransitToLadderWarp() )           { _player.StateTransfer.TransitTo( "LadderWarp", animator ); return; }
        if ( ShouldTransitToLadderJump() )           { _player.StateTransfer.TransitTo( "LadderJump", animator ); return; }
        if ( ShouldTransitToIdle()       )           { _player.StateTransfer.TransitTo( "Idle",       animator ); return; }
        if ( ShouldTransitToFall()       )           { _player.StateTransfer.TransitTo( "Fall",       animator ); return; }
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _player.Rigid.isKinematic = false;
      _player.FootCollider.isTrigger = false;
    }

    private void LadderMove() {
      bool OnlyUpKeyDown = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      if (OnlyUpKeyDown)
        _player.Movement.LadderMoveUp();

      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      if (OnlyDownKeyDown)
        _player.Movement.LadderMoveDown();
    }

    private bool ShouldTransitToLadderWarp() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool isTouched = _player.BodyCollider.IsTouchingLayers(LayerMask.GetMask("LadderTopEdge"));

      return (OnlyLeftKeyDown || OnlyRightKeyDown) && Input.GetButton("Jump") && isTouched;
    }

    private bool ShouldTransitToLadderJump() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool isNotTouched = !_player.BodyCollider.IsTouchingLayers(LayerMask.GetMask("LadderTopEdge"));

      return (OnlyLeftKeyDown || OnlyRightKeyDown) && Input.GetButton("Jump") && isNotTouched;
    }

    private bool ShouldTransitToIdle() {
      return _player.Location.IsLadderBottomEdge && _player.Location.IsGround;
    }

    private bool ShouldTransitToFall() {
      return _player.Location.IsAir && !_player.Location.IsLadder;
    }

    private Player _player;
  }
}

