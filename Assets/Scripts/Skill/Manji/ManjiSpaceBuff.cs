using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpaceBuff : Skill {
    void OnDestroy() {
      if (photonView.isMine)
        SkillReference.Instance.Remove(this);
    }

    public ParentSetter ParentSetter => _parentSetter;

    [SerializeField] private ParentSetter _parentSetter;
  }
}

