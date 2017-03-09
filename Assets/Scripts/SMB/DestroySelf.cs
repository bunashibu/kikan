using UnityEngine;
using System.Collections;

public class DestroySelf : StateMachineBehaviour {
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    Destroy(animator.gameObject);
  }
}

