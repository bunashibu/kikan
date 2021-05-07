using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
    void Awake() {
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Subscribe(_ => {
          var player = _skillUserObj.GetComponent<Player>();
          transform.position = player.transform.position + player.Weapon.AppearOffset[4];
        });

      _renderer = GetComponent<SpriteRenderer>();
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Where(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          return Client.Opponents.Contains(skillUser);
        })
        .Subscribe(_ => _renderer.color = new Color(1, 0, 0, 1));
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

      float statusRatio = 1.2f;
      float powerRatio = 1.5f;
      if (skillUser.Level.Cur.Value >= 11)
        statusRatio = 1.4f;
        powerRatio = 2.0f;

      _spaceFixSpd = new FixSpd(skillUser.Status.Spd * statusRatio, FixSpdType.Buff);
      skillUser.FixSpd.Add(_spaceFixSpd);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * statusRatio);
      skillUser.Movement.SetLadderRatio(statusRatio);

      skillUser.Synchronizer.SyncFixAtk(powerRatio);

      ResetStatus = () => {
        skillUser.FixSpd.Remove(_spaceFixSpd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);
        skillUser.Movement.SetLadderRatio(1.0f);

        skillUser.Synchronizer.SyncFixAtk(1.0f);
      };

      SkillReference.Instance.Register(viewID, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var pos = skillUser.transform.position;
      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", pos, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.SyncInit(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff.viewID, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private SpriteRenderer _renderer;

    private Action ResetStatus;
    private float _buffTime = 20.0f;
    private FixSpd _spaceFixSpd;
  }
}
