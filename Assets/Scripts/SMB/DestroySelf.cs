using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class DestroySelf : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      Destroy(animator.gameObject);
    }
  }
}

