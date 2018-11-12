using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Alt : Skill {
    void Start() {
      if (!photonView.isMine)
        return;

      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      int addition = skillUser.Level.Cur.Value * 50 + (int)(Random.value * 100);
      //skillUser.Hp.Add(2000 + addition);
      //skillUser.Hp.UpdateView();
    }
  }
}

