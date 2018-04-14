using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpaceBuff : Skill {
    public ParentSetter ParentSetter { get { return _parentSetter; } }

    [SerializeField] private ParentSetter _parentSetter;
  }
}

