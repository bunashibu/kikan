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
            NumberPopupEnvironment.Instance.PopupNumber(entity.Damage, entity.IsCritical, damageSkin, targetPhoton);
          }

          if (entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value) {
            BattleStream.OnNextKill(entity.Attacker);
            BattleStream.OnNextDie(entity.Target);
          }
        });
    }
  }
}

