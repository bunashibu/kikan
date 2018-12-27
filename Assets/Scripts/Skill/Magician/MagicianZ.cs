using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
    }

    void OnTriggerStay2D(Collider2D collider) {
    }

    private SkillSynchronizer _synchronizer;
  }
}

