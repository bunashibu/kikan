using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class SettingsStream {
    static SettingsStream() {
      _openSubject = new Subject<Unit>();
      _closeSubject = new Subject<Unit>();
    }

    public static void OnNextOpen() {
      _openSubject.OnNext(Unit.Default);
    }

    public static void OnNextClose() {
      _closeSubject.OnNext(Unit.Default);
    }

    public static IObservable<Unit> OnOpenContent => _openSubject;
    public static IObservable<Unit> OnCloseContent => _closeSubject;

    private static Subject<Unit> _openSubject;
    private static Subject<Unit> _closeSubject;
  }
}

