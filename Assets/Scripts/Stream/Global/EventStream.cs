using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class EventStream {
    static EventStream() {
      _playerInitializeSubject = new Subject<Player>();
    }

    public static void OnNextPlayerInitialize(Player player) {
      _playerInitializeSubject.OnNext(player);
    }

    public static IObservable<Player> OnPlayerInitialized => _playerInitializeSubject;

    public static Subject<Player> _playerInitializeSubject;
  }
}

