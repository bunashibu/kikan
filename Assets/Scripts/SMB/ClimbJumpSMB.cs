using UnityEngine;
using System.Collections;

public class ClimbJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _renderer = animator.GetComponent<SpriteRenderer>();
      _jump = animator.GetComponent<ClimbJump>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
    }

    Debug.Log("ClimbJump");

    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  { _linearMove.MoveLeft();  _renderer.flipX = false; }
    if (OnlyRightKeyDown) { _linearMove.MoveRight(); _renderer.flipX = true;  }
    _jump.JumpOff();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Air) {
      ActTransition("Fall", animator); return;
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("ClimbJump", false);
  }

  private RigidState _rigidState;
  private SpriteRenderer _renderer;
  private ClimbJump _jump;
  private GroundLinearMove _linearMove;
}

