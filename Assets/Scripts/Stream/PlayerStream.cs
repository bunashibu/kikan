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
      _chairSubject = new Subject<bool>();
      _fixAtkSubject = new Subject<float>();
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

    public void OnNextChair(bool shouldSit) {
      _chairSubject.OnNext(shouldSit);
    }

    public void OnNextFixAtk(float fixAtk) {
      _fixAtkSubject.OnNext(fixAtk);
    }

    public IObservable<CoreType> OnCoreLevelUpped => _coreSubject;
    public IObservable<int> OnAutoHealed => _autoHealSubject;
    public IObservable<int> OnRespawned => _respawnSubject;
    public IObservable<bool> OnChair => _chairSubject;
    public IObservable<float> OnFixAtk => _fixAtkSubject;

    private Subject<CoreType> _coreSubject;
    private Subject<int> _autoHealSubject;
    private Subject<int> _respawnSubject;
    private Subject<bool> _chairSubject;
    private Subject<float> _fixAtkSubject;
  }
}

