using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillStreamBehaviour : SingletonMonoBehaviour<SkillStreamBehaviour> {
    void Start() {
      SkillStream.OnAttacked
        .Subscribe(flow => OnAttacked(flow) )
        .AddTo(gameObject);

      SkillStream.OnDebuffed
        .Subscribe(flow => flow.Target.Debuff.DurationEnable(flow.DebuffType, flow.Duration) )
        .AddTo(gameObject);

      SkillStream.OnHealed
        .Subscribe(flow => OnHealed(flow) )
        .AddTo(gameObject);

      SkillStream.OnForced
        .Subscribe(flow => OnForced(flow) )
        .AddTo(gameObject);

      SkillStream.OnStatusFixed
        .Subscribe(flow => flow.Target.Status.SetFixAtk(flow.FixAtk) )
        .AddTo(gameObject);
    }

    private void OnAttacked(AttackFlow flow) {
      bool isAlreadyDead = flow.Target.Hp.Cur.Value == flow.Target.Hp.Min.Value;
      if (isAlreadyDead)
        return;

      int damage = flow.Target.DamageReactor.ReactTo(flow.Attacker, flow.Damage, flow.IsCritical);

      if (flow.Target is IPhotonBehaviour photonTarget) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(flow.HitEffectType, photonTarget);
        PopupNumber(flow.Attacker, photonTarget, damage, flow.IsCritical);

        // NOTE: Only died player client sends kill/death sync.
        SyncKillAndDeath(flow.Attacker, flow.Target);
      }

      if (flow.Target is Enemy enemy)
        enemy.TargetChaseSystem.SetChaseTarget(flow.Attacker.transform);
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

    private void OnHealed(HealFlow flow) {
      bool isAlreadyDead = flow.Target.Hp.Cur.Value == flow.Target.Hp.Min.Value;
      if (isAlreadyDead)
        return;

      flow.Target.Hp.Add(flow.Quantity);

      if (flow.Target is IPhotonBehaviour photonTarget) {
        HitEffectPopupEnvironment.Instance.PopupHitEffect(HitEffectType.Heal, photonTarget);
        NumberPopupEnvironment.Instance.PopupHeal(flow.Quantity, photonTarget);
      }
    }

    private void OnForced(ForceFlow flow) {
      if (flow.IsNewAdd)
        flow.Target.Rigid.velocity = new Vector2(0, 0);

      flow.Target.Rigid.AddForce(flow.Direction * flow.Force, ForceMode2D.Impulse);
    }

    [SerializeField] private BattleSynchronizer _battleSynchronizer;
  }
}
