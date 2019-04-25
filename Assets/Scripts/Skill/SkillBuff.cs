using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillBuff : Skill {
    void OnDestroy() {
      if (photonView.isMine)
        SkillReference.Instance.Remove(viewID);
    }

    public ParentSetter ParentSetter => _parentSetter;

    [SerializeField] private ParentSetter _parentSetter;
  }
}

