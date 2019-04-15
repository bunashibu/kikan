using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class WeaponStream {
    public WeaponStream() {
      _instantiateSubject = new Subject<int>();
      _curCTSubject = new Subject<CurCTFlowEntity>();
      _uniqueSubject = new Subject<int>();
    }

    public void OnNextInstantiate(int i) {
      _instantiateSubject.OnNext(i);
    }

    public void OnNextCurCT(CurCTFlowEntity entity) {
      _curCTSubject.OnNext(entity);
    }

    public void OnNextUnique(int i) {
      _uniqueSubject.OnNext(i);
    }

    public IObservable<int> OnInstantiated => _instantiateSubject;
    public IObservable<CurCTFlowEntity> OnCurCT => _curCTSubject;
    public IObservable<int> OnUniqueUsed => _uniqueSubject;

    private Subject<int> _instantiateSubject;
    private Subject<CurCTFlowEntity> _curCTSubject;
    private Subject<int> _uniqueSubject;
  }

  public class CurCTFlowEntity {
    public CurCTFlowEntity(int index, float curCT) {
      Index = index;
      CurCT = curCT;
    }

    public int Index { get; private set; }
    public float CurCT { get; private set; }
  }
}

