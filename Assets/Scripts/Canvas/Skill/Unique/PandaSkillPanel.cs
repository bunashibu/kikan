using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class PandaSkillPanel : SkillPanel {
    public override void Register(Weapon weapon) {
      base.Register(weapon);

      weapon.Stream.OnUniqueUsed
        .Subscribe(index => {

        })
        .AddTo(weapon.gameObject);
    }
  }
}

