using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IOnAttacked {
    Hp Hp { get; }
    Action<IAttacker, int, bool, HitEffectType> OnAttacked { get; }
    Action<IAttacker>                           OnKilled   { get; } // tmp
  }
}

