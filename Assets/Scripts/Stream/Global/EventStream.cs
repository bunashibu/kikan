using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public static class EventStream {
    static EventStream() {
      _playerInitializeSubject = new Subject<Player>();
      _clientPlayerDieSubject = new Subject<Unit>();
    }

    public static void OnNextPlayerInitialize(Player player) {
      _playerInitializeSubject.OnNext(player);
    }

    public static void OnNextClientPlayerDie() {
      _clientPlayerDieSubject.OnNext(Unit.Default);
    }

    public static IObservable<Player> OnPlayerInitialized => _playerInitializeSubject;
    public static IObservable<Unit> OnClientPlayerDied => _clientPlayerDieSubject;

    public static Subject<Player> _playerInitializeSubject;
    public static Subject<Unit> _clientPlayerDieSubject;

    // Currently _clientPlayerDieSubject is only used for watching final battle
    // It should be used for wide variety thing
  }
}
