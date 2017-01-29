using UnityEngine;
using System.Collections;

public class WalkRightSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
    }

    Debug.Log("right");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _linearMove.MoveRight();

    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool LeftKeyDown      = Input.GetKeyDown(KeyCode.LeftArrow);
    bool RightKeyUp       = Input.GetKeyUp(KeyCode.RightArrow);

    if (OnlyLeftKeyDown) {
      animator.SetBool("WalkLeft", true);
      animator.SetBool("WalkRight", false);
      return;
    }

    if (JumpButtonDown) {
      animator.SetBool("GroundJump", true);
      animator.SetBool("WalkRight", false);
      return;
    }

    if (RightKeyUp || LeftKeyDown) {
      animator.SetTrigger("ToIdle");
      animator.SetBool("WalkRight", false);
      return;
    }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
    animator.SetBool("WalkRight", false);
  }

  private RigidState _rigidState;
  private GroundLinearMove _linearMove;
}

