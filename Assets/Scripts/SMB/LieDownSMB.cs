using UnityEngine;
using System.Collections;

public class LieDownSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground) {
      GroundUpdate(animator);
      return;
    }

    if (_rigidState.Air) {
      AirUpdate(animator);
      return;
    }
  }

  private void GroundUpdate(Animator animator) {
    if (!Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
      animator.SetBool("Idle", true);
      animator.SetBool("LieDown", false);
      return;
    }

    if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
      animator.SetBool("WalkLeft", true);
      animator.SetBool("LieDown", false);
      return;
    }

    if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
      animator.SetBool("WalkRight", true);
      animator.SetBool("LieDown", false);
      return;
    }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
    animator.SetBool("Idle", false);
  }

  private RigidState _rigidState;
}

