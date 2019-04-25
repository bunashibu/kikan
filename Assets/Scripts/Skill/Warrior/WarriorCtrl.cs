using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class WarriorCtrl : Skill {
    void Start() {
      transform.parent = _skillUserObj.transform;
    }
  }
}

