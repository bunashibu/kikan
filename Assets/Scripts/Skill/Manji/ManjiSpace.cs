using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
    void Start() {
      transform.parent = _skillUserObj.transform;

      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
      skillUser.Movement.SetMoveForce(skillUser.Status.Spd * 1.3f);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * 1.3f);

      skillUser.Status.MultipleMulCorrectionAtk(1.5f);

      MonoUtility.Instance.DelaySec(20.0f, () => {
        skillUser.Movement.SetMoveForce(skillUser.Status.Spd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);

        skillUser.Status.ResetMulCorrectionAtk();
      });
    }
  }
}

