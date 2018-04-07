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
      skillUser.Movement.SetMoveForce(skillUser.Status.Spd * 1.3f);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * 1.3f);

      skillUser.Status.MultipleMulCorrectionAtk(1.5f);

      MonoUtility.Instance.DelaySec(20.0f, () => {
        skillUser.Movement.SetMoveForce(skillUser.Status.Spd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);

        skillUser.Status.ResetMulCorrectionAtk();
      });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<ParentSetter>() as ParentSetter;
      buff.SetParent(skillUser.PhotonView.viewID);

      MonoUtility.Instance.DelaySec(20.0f, () => {
          PhotonNetwork.Destroy(buff.gameObject);
      });
    }
  }
}

