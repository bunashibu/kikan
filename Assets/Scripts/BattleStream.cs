using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class BattleStream {
    static BattleStream() {
      _killSubject = new Subject<Player>();
      _dieSubject = new Subject<Player>();
    }

    public static void OnNextKill(Player killPlayer) {
      _killSubject.OnNext(killPlayer);
    }

    public static void OnNextDie(Player deathPlayer) {
      _dieSubject.OnNext(deathPlayer);
    }

    public static IObservable<IList<Player>> OnKilledAndDied => Observable.Zip(_killSubject, _dieSubject);

    private static Subject<Player> _killSubject;
    private static Subject<Player> _dieSubject;
  }
}

