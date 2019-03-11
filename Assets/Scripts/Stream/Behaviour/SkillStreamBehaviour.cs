using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillStreamBehaviour : SingletonMonoBehaviour<SkillStreamBehaviour> {
    void Awake() {
      _battleSynchronizer = GetComponent<BattleSynchronizer>();
    }

    void Start() {
      SkillStream.OnAttacked
        .Subscribe(entity => {
          entity.Target.Hp.Subtract(entity.Damage);

          if (entity.Target is IPhotonBehaviour) {
            var targetPhoton = (IPhotonBehaviour)entity.Target;
            int damageSkin = 0;

            if (entity.Target is IDamageSkin)
              damageSkin = ((IDamageSkin)entity.Attacker).DamageSkinId;

            HitEffectPopupEnvironment.Instance.PopupHitEffect(entity.HitEffectType, targetPhoton);
            NumberPopupEnvironment.Instance.PopupDamage(entity.Damage, entity.IsCritical, damageSkin, targetPhoton);

            bool isDead = entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value;

            if (targetPhoton.PhotonView.isMine && isDead) {
              _battleSynchronizer.SyncKill(entity.Attacker);
              _battleSynchronizer.SyncDie(entity.Target);
            }
          }

          if (entity.Target is Enemy) {
            var enemy = (Enemy)entity.Target;
            enemy.TargetChaseSystem.SetChaseTarget(entity.Attacker.transform);
          }
        })
        .AddTo(gameObject);

      SkillStream.OnDebuffed
        .Subscribe(entity => {
          entity.Target.Debuff.DurationEnable(entity.DebuffType, entity.Duration);
        })
        .AddTo(gameObject);

      SkillStream.OnHealed
        .Subscribe(entity => {
          entity.Target.Hp.Add(entity.Quantity);

          if (entity.Target is IPhotonBehaviour) {
            var targetPhoton = (IPhotonBehaviour)entity.Target;
            HitEffectPopupEnvironment.Instance.PopupHitEffect(HitEffectType.Heal, targetPhoton);
            NumberPopupEnvironment.Instance.PopupHeal(entity.Quantity, targetPhoton);
          }
        })
        .AddTo(gameObject);

      SkillStream.OnForced
        .Subscribe(entity => {
          entity.Target.Rigid.AddForce(entity.Direction * entity.Force, ForceMode2D.Impulse);
        })
        .AddTo(gameObject);

      SkillStream.OnStatusFixed
        .Subscribe(entity => {
          entity.Target.Status.SetFixAtk(entity.FixAtk);
        })
        .AddTo(gameObject);
    }

    private BattleSynchronizer _battleSynchronizer;
  }
}

