using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class NageShift : Skill {
    void Awake() {
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => transform.parent = _skillUserObj.transform );
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

      skillUser.Synchronizer.SyncFixCritical(10);

      ResetStatus = () => {
        skillUser.Synchronizer.SyncFixCritical(0);
      };

      SkillReference.Instance.Register(viewID, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Nage/ShiftBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.ParentSetter.SetParent(skillUser.PhotonView.viewID);
      buff.transform.Translate(new Vector2(0, 1.1f));

      SkillReference.Instance.Register(buff.viewID, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
    private float _buffTime = 6.0f;
  }
}

