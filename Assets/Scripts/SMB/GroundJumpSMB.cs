using UnityEngine;
using System.Collections;

public class GroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
      _jump = animator.GetComponent<GroundJump>();
    }

    Debug.Log("jump");
    _jump.Jump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");

    if (JumpButtonDown) {
      _jump.Jump();

      if (OnlyLeftKeyDown)  _linearMove.MoveLeft();
      if (OnlyRightKeyDown) _linearMove.MoveRight();

      return;
    }

    if (OnlyLeftKeyDown) {
      animator.SetBool("WalkLeft", true);
      animator.SetBool("GroundJump", false);
      return;
    }

    if (OnlyRightKeyDown) {
      animator.SetBool("WalkRight", true);
      animator.SetBool("GroundJump", false);
      return;
    }

    animator.SetTrigger("ToIdle");
    animator.SetBool("GroundJump", false);
  }

  private RigidState _rigidState;
  private GroundJump _jump;
  private GroundLinearMove _linearMove;
}

