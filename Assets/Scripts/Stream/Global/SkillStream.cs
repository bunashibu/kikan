using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class SkillStream {
    static SkillStream() {
      _attackSubject = new Subject<AttackFlow>();
      _debuffSubject = new Subject<DebuffFlow>();
      _healSubject = new Subject<HealFlow>();
      _forceSubject = new Subject<ForceFlow>();
      _statusSubject = new Subject<StatusFlow>();
    }

    public static void OnNextAttack(AttackFlow AttackFlow) {
      _attackSubject.OnNext(AttackFlow);
    }

    public static void OnNextDebuff(DebuffFlow DebuffFlow) {
      _debuffSubject.OnNext(DebuffFlow);
    }

    public static void OnNextHeal(HealFlow HealFlow) {
      _healSubject.OnNext(HealFlow);
    }

    public static void OnNextForce(ForceFlow ForceFlow) {
      _forceSubject.OnNext(ForceFlow);
    }

    public static void OnNextStatus(StatusFlow StatusFlow) {
      _statusSubject.OnNext(StatusFlow);
    }

    public static IObservable<AttackFlow> OnAttacked => _attackSubject;
    public static IObservable<DebuffFlow> OnDebuffed => _debuffSubject;
    public static IObservable<HealFlow> OnHealed => _healSubject;
    public static IObservable<ForceFlow> OnForced => _forceSubject;
    public static IObservable<StatusFlow> OnStatusFixed => _statusSubject;

    private static Subject<AttackFlow> _attackSubject;
    private static Subject<DebuffFlow> _debuffSubject;
    private static Subject<HealFlow> _healSubject;
    private static Subject<ForceFlow> _forceSubject;
    private static Subject<StatusFlow> _statusSubject;
  }

  public class AttackFlow {
    public AttackFlow(IAttacker attacker, IOnAttacked target, int damage, bool isCritical, HitEffectType hitEffectType) {
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

  public class DebuffFlow {
    public DebuffFlow(IOnDebuffed target, DebuffType debuffType, float duration) {
      Target     = target;
      DebuffType = debuffType;
      Duration   = duration;
    }

    public IOnDebuffed Target     { get; private set; }
    public DebuffType  DebuffType { get; private set; }
    public float       Duration   { get; private set; }
  }

  public class HealFlow {
    public HealFlow(IOnAttacked target, int quantity) {
      Target   = target;
      Quantity = quantity;
    }

    public IOnAttacked Target   { get; private set; }
    public int         Quantity { get; private set; }
  }

  public class ForceFlow {
    public ForceFlow(IOnForced target, float force, Vector2 direction, bool isNewAdd) {
      Target    = target;
      Force     = force;
      Direction = direction;
      IsNewAdd  = isNewAdd;
    }

    public IOnForced Target    { get; private set; }
    public float     Force     { get; private set; }
    public Vector2   Direction { get; private set; }
    public bool      IsNewAdd  { get; private set; }
  }

  public class StatusFlow {
    public StatusFlow(IStatus target, int fixAtk) {
      Target = target;
      FixAtk = fixAtk;
    }

    public IStatus Target { get; private set; }
    public int     FixAtk { get; private set; }
  }
}
