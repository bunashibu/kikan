using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
    void Start() {
      EnhanceStatus();

      if (photonView.isMine)
        InstantiateBuff();
    }

    private void EnhanceStatus() {
      transform.parent = _skillUserObj.transform;

      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      float statusRatio = 1.3f;
      float powerRatio = 1.5f;
      if (skillUser.Level.Lv >= 11)
        statusRatio = 1.6f;
        powerRatio = 2.0f;

      skillUser.Movement.SetMoveForce(skillUser.Status.Spd * statusRatio);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * statusRatio);

      skillUser.Status.MultipleMulCorrectionAtk(powerRatio);

      MonoUtility.Instance.StoppableDelaySec(20.0f, "ManjiSpace" + GetInstanceID().ToString(), () => {
        skillUser.Movement.SetMoveForce(skillUser.Status.Spd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);

        skillUser.Status.ResetMulCorrectionAtk();
      });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<ParentSetter>() as ParentSetter;
      buff.SetParent(skillUser.PhotonView.viewID);

      MonoUtility.Instance.StoppableDelaySec(20.0f, "ManjiBuff", () => {
          PhotonNetwork.Destroy(buff.gameObject);
      });
    }
  }
}

