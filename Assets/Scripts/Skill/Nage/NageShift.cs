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
        .Subscribe(_ => {
          var player = _skillUserObj.GetComponent<Player>();
          transform.position = player.transform.position + player.Weapon.AppearOffset[1];
        });

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Where(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          return Client.Opponents.Contains(skillUser);
        })
        .Subscribe(_ => _renderer.color = new Color(0, 1, 1, 1));
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

      var pos = skillUser.transform.position + new Vector3(0, 1.1f, 0);
      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Nage/ShiftBuff", pos, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.SyncInit(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff.viewID, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    [SerializeField] private SpriteRenderer _renderer;

    private Action ResetStatus;
    private float _buffTime = 6.0f;
  }
}
