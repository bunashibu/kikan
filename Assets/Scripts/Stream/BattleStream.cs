using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class BattleStream {
    static BattleStream() {
      _killSubject = new Subject<IAttacker>();
      _dieSubject = new Subject<IOnAttacked>();
    }

    public static void OnNextKill(IAttacker attacker) {
      _killSubject.OnNext(attacker);
    }

    public static void OnNextDie(IOnAttacked target) {
      _dieSubject.OnNext(target);
    }

    public static IObservable<KillDeathFlowEntity> OnKilledAndDied { get {
      return Observable.Zip(_killSubject, _dieSubject, (attacker, target) => new KillDeathFlowEntity(attacker, target));
    } }

    private static Subject<IAttacker> _killSubject;
    private static Subject<IOnAttacked> _dieSubject;
  }

  public class KillDeathFlowEntity {
    public KillDeathFlowEntity(IAttacker attacker, IOnAttacked target) {
      Attacker = attacker;
      Target   = target;
    }

    public IAttacker   Attacker { get; private set; }
    public IOnAttacked Target   { get; private set; }
  }
}

