using UnityEngine;
using System.Collections;

public class LieDownSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();

    Debug.Log("liedown");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool DownKeyUp        = Input.GetKeyUp(KeyCode.DownArrow);
    bool UpKeyDown        = Input.GetKeyDown(KeyCode.UpArrow);

    if (OnlyLeftKeyDown) {
      animator.SetBool("WalkLeft", true);
      animator.SetBool("LieDown", false);
      return;
    }

    if (OnlyRightKeyDown) {
      animator.SetBool("WalkRight", true);
      animator.SetBool("LieDown", false);
      return;
    }

    if (JumpButtonDown) {
      animator.SetBool("ToStepDownJump", true);
      animator.SetBool("LieDown", false);
      return;
    }

    if (DownKeyUp || UpKeyDown) {
      animator.SetTrigger("ToIdle");
      animator.SetBool("LieDown", false);
      return;
    }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
    animator.SetBool("LieDown", false);
  }

  private RigidState _rigidState;
}

