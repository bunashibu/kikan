using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class PlayerStream {
    public PlayerStream() {
      _coreSubject = new Subject<CoreType>();
      _autoHealSubject = new Subject<int>();
      _respawnSubject = new Subject<int>();
      _killSubject = new Subject<Player>();
      _dieSubject = new Subject<Player>();
    }

    public void OnNextCore(CoreType type) {
      _coreSubject.OnNext(type);
    }

    public void OnNextAutoHeal(int quantity) {
      _autoHealSubject.OnNext(quantity);
    }

    public void OnNextRespawn(int viewID) {
      _respawnSubject.OnNext(viewID);
    }

    public void OnNextKill(Player killPlayer) {
      _killSubject.OnNext(killPlayer);
    }

    public void OnNextDie(Player deathPlayer) {
      _dieSubject.OnNext(deathPlayer);
    }

    public IObservable<CoreType> OnCoreLevelUpped => _coreSubject;
    public IObservable<int> OnAutoHealed => _autoHealSubject;
    public IObservable<int> OnRespawned => _respawnSubject;
    public IObservable<IList<Player>> OnKilledAndDied => Observable.Zip(_killSubject, _dieSubject);

    private Subject<CoreType> _coreSubject;
    private Subject<int> _autoHealSubject;
    private Subject<int> _respawnSubject;
    private Subject<Player> _killSubject;
    private Subject<Player> _dieSubject;
  }
}

