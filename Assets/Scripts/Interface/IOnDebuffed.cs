using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IOnDebuffed {
    Action<DebuffType, float> OnDebuffed  { get; }
    ReactiveState<DebuffType> DebuffState { get; }
  }
}

