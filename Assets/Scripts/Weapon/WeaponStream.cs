using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class WeaponStream {
    public WeaponStream() {
      _instantiateSubject = new Subject<int>();
    }

    public void OnNextInstantiate(int i) {
      _instantiateSubject.OnNext(i);
    }

    public IObservable<int> OnInstantiated => _instantiateSubject;

    private Subject<int> _instantiateSubject;
  }
}

