using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattle : IPhoton {
    Hp            Hp        { get; }
    List<IBattle> Teammates { get; }

    int KillExp      { get; }
    int KillGold     { get; }
    int DamageSkinId { get; }
    int Power        { get; }
    int Critical     { get; }
    string Tag       { get; }

    Action<IBattle, int, bool> OnAttacked { get; }
    Action<IBattle>            OnKilled   { get; }
  }
}

