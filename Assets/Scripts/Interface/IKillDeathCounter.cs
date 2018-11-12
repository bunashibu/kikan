using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public interface IKillDeathCounter {
    ReactiveProperty<int> KillCount  { get; }
    ReactiveProperty<int> DeathCount { get; }
  }
}

