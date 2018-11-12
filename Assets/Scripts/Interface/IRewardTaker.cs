using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IRewardTaker {
    Exp           Exp       { get; }
    Gold          Gold      { get; }
    List<IBattle> Teammates { get; }
  }
}

