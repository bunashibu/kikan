using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DestroySkillSelf : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      // INFO: Only make a sense if you are a skill owner
      var skill = animator.gameObject.GetComponent<Skill>();
      SkillReference.Instance.Remove(skill.viewID);

      // NOTE: In order to prevent that skill is deleted before SkillSynchronizer-RPC finishes synchronization because of lag,
      //       wait 5.0f and delete it.
      animator.gameObject.SetActive(false);
      Destroy(animator.gameObject, 5.0f);
    }
  }
}

