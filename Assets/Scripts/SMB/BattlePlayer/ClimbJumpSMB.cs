using UnityEngine;
using System.Collections;

public class ClimbJumpSMB : BattlePlayerSMB {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    base.OnStateEnter(animator, stateInfo, layerIndex);
    _currentStateName = "ClimbJump";
    Debug.Log("ClimbJump");

    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  { _movement.GroundMoveLeft(); foreach (var sprite in _renderers) sprite.flipX = false; }
    if (OnlyRightKeyDown) { _movement.GroundMoveRight(); foreach (var sprite in _renderers) sprite.flipX = true; }

    _movement.ClimbJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      if ( ShouldTransitionTo( "Die"  ) )  { ActTransition ( "Die"  , animator ); return; }
      if ( ShouldTransitionTo( "Fall" ) )  { ActTransition ( "Fall" , animator ); return; }
    }
  }
}

