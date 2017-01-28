using UnityEngine;
using System.Collections;

public class FallSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    animator.SetBool("Idle", true);
    animator.SetBool("Fall", false);
  }

  private RigidState _rigidState;
}

