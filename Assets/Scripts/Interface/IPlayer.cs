using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public interface IPlayer : ICharacter, IBattle, ISpeaker {
    Exp           Exp       { get; }
    Gold          Gold      { get; }
    List<IPlayer> Teammates { get; }

    ReactiveProperty<int> KillCount  { get; }
    ReactiveProperty<int> DeathCount { get; }
  }
}

