using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class SkillStream {
    static SkillStream() {
      _attackSubject = new Subject<AttackFlowEntity>();
      _debuffSubject = new Subject<DebuffFlowEntity>();
      _healSubject = new Subject<HealFlowEntity>();
    }

    public static void OnNextAttack(AttackFlowEntity attackFlowEntity) {
      _attackSubject.OnNext(attackFlowEntity);
    }

    public static void OnNextDebuff(DebuffFlowEntity debuffFlowEntity) {
      _debuffSubject.OnNext(debuffFlowEntity);
    }

    public static void OnNextHeal(HealFlowEntity healFlowEntity) {
      _healSubject.OnNext(healFlowEntity);
    }

    public static IObservable<AttackFlowEntity> OnAttacked => _attackSubject;
    public static IObservable<DebuffFlowEntity> OnDebuffed => _debuffSubject;
    public static IObservable<HealFlowEntity> OnHealed => _healSubject;

    private static Subject<AttackFlowEntity> _attackSubject;
    private static Subject<DebuffFlowEntity> _debuffSubject;
    private static Subject<HealFlowEntity> _healSubject;
  }

  public class AttackFlowEntity {
    public AttackFlowEntity(IAttacker attacker, IOnAttacked target, int damage, bool isCritical, HitEffectType hitEffectType) {
      Attacker      = attacker;
      Target        = target;
      Damage        = damage;
      IsCritical    = isCritical;
      HitEffectType = hitEffectType;
    }

    public IAttacker     Attacker      { get; private set; }
    public IOnAttacked   Target        { get; private set; }
    public int           Damage        { get; private set; }
    public bool          IsCritical    { get; private set; }
    public HitEffectType HitEffectType { get; private set; }
  }

  public class DebuffFlowEntity {
    public DebuffFlowEntity(IOnDebuffed target, DebuffType debuffType, float duration) {
      Target     = target;
      DebuffType = debuffType;
      Duration   = duration;
    }

    public IOnDebuffed Target     { get; private set; }
    public DebuffType  DebuffType { get; private set; }
    public float       Duration   { get; private set; }
  }

  public class HealFlowEntity {
    public HealFlowEntity(IOnAttacked target, int quantity) {
      Target   = target;
      Quantity = quantity;
    }

    public IOnAttacked Target   { get; private set; }
    public int         Quantity { get; private set; }
  }
}

