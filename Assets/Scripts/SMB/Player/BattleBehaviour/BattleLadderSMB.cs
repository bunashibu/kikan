using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleLadderSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();

      _player.Rigid.isKinematic = true;
      _player.Rigid.velocity = new Vector2(0.0f, 0.0f);
      _player.FootCollider.isTrigger = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        LadderMove();

        if ( _player.Hp.Cur <= 0    ) { _player.StateTransfer.TransitTo( "Die" , animator ); return; }
        if ( _player.BuffState.Stun ) { _player.StateTransfer.TransitTo( "Stun", animator ); return; }

        if ( ShouldTransitToLadderJump() ) { TransitToLadderJump(animator);                               return; }
        if ( ShouldTransitToIdle()       ) { _player.StateTransfer.TransitTo ( "Idle"       , animator ); return; }
        if ( ShouldTransitToFall()       ) { _player.StateTransfer.TransitTo ( "Fall"       , animator ); return; }
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

    private bool ShouldTransitToLadderJump() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

      return (OnlyLeftKeyDown || OnlyRightKeyDown) && Input.GetButton("Jump");
    }

    private bool ShouldTransitToIdle() {
      return _player.State.LadderBottomEdge && _player.State.Ground;
    }

    private bool ShouldTransitToFall() {
      return _player.State.Air && !_player.State.Ladder;
    }

    private void TransitToLadderJump(Animator animator) {
      LadderJump();
      _player.StateTransfer.TransitTo ("LadderJump", animator);
    }

    private void LadderJump() {
      _player.Rigid.isKinematic = false;
      _player.FootCollider.isTrigger = false;

      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.GroundMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.GroundMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }

      _player.Movement.LadderJump();
    }

    private BattlePlayer _player;
  }
}

