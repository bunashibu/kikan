using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
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

      _statusRatio = 1.3f;
      float powerRatio = 1.5f;
      if (skillUser.Level.Cur.Value >= 11)
        _statusRatio = 1.6f;
        powerRatio = 2.0f;

      skillUser.FixSpd.Add(skillUser.Status.Spd * _statusRatio);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * _statusRatio);
      skillUser.Movement.SetLadderRatio(_statusRatio);

      skillUser.Synchronizer.SyncFixAtk(powerRatio);

      ResetStatus = () => {
        skillUser.FixSpd.Remove(skillUser.Status.Spd * _statusRatio);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);
        skillUser.Movement.SetLadderRatio(1.0f);

        skillUser.Synchronizer.SyncFixAtk(1.0f);
      };

      SkillReference.Instance.Register(this, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.ParentSetter.SetParent(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
    private float _buffTime = 20.0f;
    private float _statusRatio;
  }
}

