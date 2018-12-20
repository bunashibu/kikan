using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class Alt : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
    }

    void Start() {
      if (!photonView.isMine)
        return;

      var skillUser = _skillUserObj.GetComponent<Player>();
      int addition = skillUser.Level.Cur.Value * 50 + (int)(Random.value * 100);

      _synchronizer.SyncHeal(_skillUserViewID, 2000 + addition);
    }

    private SkillSynchronizer _synchronizer;
  }
}

