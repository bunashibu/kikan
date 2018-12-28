using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillInitializer : SingletonMonoBehaviour<SkillInitializer> {
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
          }

          if (entity.Target is Enemy) {
            var enemy = (Enemy)entity.Target;
            enemy.TargetChaseSystem.SetChaseTarget(entity.Attacker.transform);
          }

          if (entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value) {
            BattleStream.OnNextKill(entity.Attacker);
            BattleStream.OnNextDie(entity.Target);
          }
        });

      SkillStream.OnDebuffed
        .Subscribe(entity => {
          entity.Target.Debuff.DurationEnable(entity.DebuffType, entity.Duration);
        });

      SkillStream.OnHealed
        .Subscribe(entity => {
          entity.Target.Hp.Add(entity.Quantity);

          if (entity.Target is IPhotonBehaviour) {
            var targetPhoton = (IPhotonBehaviour)entity.Target;
            NumberPopupEnvironment.Instance.PopupHeal(entity.Quantity, targetPhoton);
          }
        });

      SkillStream.OnForced
        .Subscribe(entity => {
          entity.Target.Rigid.AddForce(entity.Direction * entity.Force, ForceMode2D.Impulse);
        });
    }
  }
}

