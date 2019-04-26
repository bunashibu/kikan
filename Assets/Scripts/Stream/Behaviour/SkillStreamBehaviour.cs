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
        .Subscribe(entity => OnAttacked(entity) )
        .AddTo(gameObject);

      SkillStream.OnDebuffed
        .Subscribe(entity => entity.Target.Debuff.DurationEnable(entity.DebuffType, entity.Duration) )
        .AddTo(gameObject);

      SkillStream.OnHealed
        .Subscribe(entity => OnHealed(entity) )
        .AddTo(gameObject);

      SkillStream.OnForced
        .Subscribe(entity => OnForced(entity) )
        .AddTo(gameObject);

      SkillStream.OnStatusFixed
        .Subscribe(entity => entity.Target.Status.SetFixAtk(entity.FixAtk) )
        .AddTo(gameObject);
    }

    private void OnAttacked(AttackFlowEntity entity) {
      bool isAlreadyDead = entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value;
      if (isAlreadyDead)
        return;

      ApplyDamage(entity.Target, entity.Damage);

      if (entity.Target is IPhotonBehaviour) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(entity.HitEffectType, (IPhotonBehaviour)entity.Target);
        PopupNumber(entity.Attacker, (IPhotonBehaviour)entity.Target, entity.Damage, entity.IsCritical);

        // Only died player client does.
        SyncKillAndDeath(entity.Attacker, entity.Target);
      }

      if (entity.Target is Enemy) {
        var enemy = (Enemy)entity.Target;
        enemy.TargetChaseSystem.SetChaseTarget(entity.Attacker.transform);
      }
    }

    private void ApplyDamage(IOnAttacked target, int damage) {
      target.Hp.Subtract(damage);
    }

    private void PopupNumber(IAttacker attacker, IPhotonBehaviour targetPhoton, int damage, bool isCritical) {
      int damageSkin = 0;

      if (attacker is IDamageSkin)
        damageSkin = ((IDamageSkin)attacker).DamageSkinId;

      NumberPopupEnvironment.Instance.PopupDamage(damage, isCritical, damageSkin, targetPhoton);
    }

    private void SyncKillAndDeath(IAttacker attacker, IOnAttacked target) {
      bool isDead = target.Hp.Cur.Value == target.Hp.Min.Value;

      if ( ((IPhotonBehaviour)target).PhotonView.isMine && isDead ) {
        _battleSynchronizer.SyncKill(attacker);
        _battleSynchronizer.SyncDie(target);
      }
    }

    private void OnHealed(HealFlowEntity entity) {
      bool isAlreadyDead = entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value;
      if (isAlreadyDead)
        return;

      entity.Target.Hp.Add(entity.Quantity);

      if (entity.Target is IPhotonBehaviour) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(HitEffectType.Heal, (IPhotonBehaviour)entity.Target);
        NumberPopupEnvironment.Instance.PopupHeal(entity.Quantity, (IPhotonBehaviour)entity.Target);
      }
    }

    private void OnForced(ForceFlowEntity entity) {
      if (entity.IsNewAdd)
        entity.Target.Rigid.velocity = new Vector2(0, 0);

      entity.Target.Rigid.AddForce(entity.Direction * entity.Force, ForceMode2D.Impulse);
    }

    private BattleSynchronizer _battleSynchronizer;
  }
}

