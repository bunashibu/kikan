using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DestroySkillSelf : StateMachineBehaviour {
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      var skill = animator.gameObject.GetComponent<Skill>();
      SkillReference.Instance.Remove(skill);

      Destroy(animator.gameObject);
    }
  }
}

