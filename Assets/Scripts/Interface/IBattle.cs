using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattle {
    Hp Hp { get; }
    BattlePlayerObserver Observer { get; }
  }
}

