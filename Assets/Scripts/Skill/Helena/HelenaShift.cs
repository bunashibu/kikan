using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class HelenaShift : Skill {
    void Awake() {
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Subscribe(_ => {
          var player = _skillUserObj.GetComponent<Player>();
          transform.position = player.transform.position + player.Weapon.AppearOffset[1];
        });
    }

    void Start() {
      if (photonView.isMine) {
        EnhanceStatus();
        InstantiateBuff();
      }
    }

    void OnDestroy() {
      if (photonView.isMine) {
        ResetStatus();
        SkillReference.Instance.Remove(viewID);
      }
    }

    private void EnhanceStatus() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      _shiftFixSpd = new FixSpd(skillUser.Status.Spd *_spdRatio, FixSpdType.Buff);
      skillUser.FixSpd.Add(_shiftFixSpd);
      skillUser.Movement.SetLadderRatio(_spdRatio);

      ResetStatus = () => {
        skillUser.FixSpd.Remove(_shiftFixSpd);
        skillUser.Movement.SetLadderRatio(1.0f);
      };

      SkillReference.Instance.Register(viewID, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var pos = skillUser.transform.position + new Vector3(0, 0, 0);
      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Helena/ShiftBuff", pos, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.SyncInit(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff.viewID, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
    private FixSpd _shiftFixSpd;
    private float _spdRatio = 1.1f;
    private float _buffTime = 6.0f;
  }
}
