using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Alt : Skill {
    void Start() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
      skillUser.Hp.Add(2000);
      skillUser.Hp.UpdateView();
    }
  }
}

