using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NageShift : Skill {
    void Start() {
      transform.parent = _skillUserObj.transform;

      if (photonView.isMine) {
        EnhanceStatus();
        InstantiateBuff();
      }
    }

    void OnDestroy() {
      if (photonView.isMine) {
        ResetStatus();
        SkillReference.Instance.Remove(this);
      }
    }

    private void EnhanceStatus() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      skillUser.Status.SetFixCritical(10);

      ResetStatus = () => {
        skillUser.Status.SetFixCritical(0);
      };

      SkillReference.Instance.Register(this, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Nage/ShiftBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.ParentSetter.SetParent(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
    private float _buffTime = 5.0f;
  }
}

