﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillStreamBehaviour : SingletonMonoBehaviour<SkillStreamBehaviour> {
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

      int damage = entity.Target.DamageReactor.ReactTo(entity.Damage);

      if (entity.Target is IPhotonBehaviour photonTarget) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(entity.HitEffectType, photonTarget);
        PopupNumber(entity.Attacker, photonTarget, damage, entity.IsCritical);

        // NOTE: Only died player client sends kill/death sync.
        SyncKillAndDeath(entity.Attacker, entity.Target);
      }

      if (entity.Target is Enemy enemy)
        enemy.TargetChaseSystem.SetChaseTarget(entity.Attacker.transform);
    }

    private void PopupNumber(IAttacker attacker, IPhotonBehaviour targetPhoton, int damage, bool isCritical) {
      int damageSkin = 0;

      // tmp: IDamageSkin is strange interface
      if (attacker is IDamageSkin skin)
        damageSkin = skin.DamageSkinId;

      NumberPopupEnvironment.Instance.PopupDamage(damage, isCritical, damageSkin, targetPhoton);
    }

    private void SyncKillAndDeath(IAttacker attacker, IOnAttacked target) {
      bool isDead = target.Hp.Cur.Value == target.Hp.Min.Value;

      if (target is IPhotonBehaviour photonTarget) {
        if (photonTarget.PhotonView.isMine && isDead) {
          _battleSynchronizer.SyncKill(attacker);
          _battleSynchronizer.SyncDie(target);
        }
      }
    }

    private void OnHealed(HealFlowEntity entity) {
      bool isAlreadyDead = entity.Target.Hp.Cur.Value == entity.Target.Hp.Min.Value;
      if (isAlreadyDead)
        return;

      entity.Target.Hp.Add(entity.Quantity);

      if (entity.Target is IPhotonBehaviour photonTarget) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(HitEffectType.Heal, photonTarget);
        NumberPopupEnvironment.Instance.PopupHeal(entity.Quantity, photonTarget);
      }
    }

    private void OnForced(ForceFlowEntity entity) {
      if (entity.IsNewAdd)
        entity.Target.Rigid.velocity = new Vector2(0, 0);

      entity.Target.Rigid.AddForce(entity.Direction * entity.Force, ForceMode2D.Impulse);
    }

    [SerializeField] private BattleSynchronizer _battleSynchronizer;
  }
}

