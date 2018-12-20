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

    public IObservable<CoreType> OnCoreLevelUpped => _coreSubject;
    public IObservable<int> OnAutoHealed => _autoHealSubject;
    public IObservable<int> OnRespawned => _respawnSubject;

    private Subject<CoreType> _coreSubject;
    private Subject<int> _autoHealSubject;
    private Subject<int> _respawnSubject;
  }
}

