using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class WeaponStream {
    public WeaponStream() {
      _instantiateSubject = new Subject<int>();
      _curCTSubject = new Subject<CurCTFlow>();
      _uniqueSubject = new Subject<int>();
      _readySubject = new Subject<int>();
      _availableSubject = new Subject<int>();
    }

    public void OnNextInstantiate(int i) {
      _instantiateSubject.OnNext(i);
    }

    public void OnNextCurCT(CurCTFlow flow) {
      _curCTSubject.OnNext(flow);
    }

    public void OnNextUnique(int i) {
      _uniqueSubject.OnNext(i);
    }

    public void OnNextSkillIsReady(int i) {
      _readySubject.OnNext(i);
    }

    public void OnNextSkillAvailable(int i) {
      _availableSubject.OnNext(i);
    }

    public IObservable<int> OnInstantiated => _instantiateSubject;
    public IObservable<CurCTFlow> OnCurCT => _curCTSubject;
    public IObservable<int> OnUniqueUsed => _uniqueSubject;
    public IObservable<int> OnSkillIsReady  => _readySubject;
    public IObservable<int> OnSkillAvailable => _availableSubject;

    private Subject<int> _instantiateSubject;
    private Subject<CurCTFlow> _curCTSubject;
    private Subject<int> _uniqueSubject;
    private Subject<int> _readySubject;
    private Subject<int> _availableSubject;
  }

  public class CurCTFlow {
    public CurCTFlow(int index, float curCT) {
      Index = index;
      CurCT = curCT;
    }

    public int Index { get; private set; }
    public float CurCT { get; private set; }
  }
}
