using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IRewardTaker : IMediatorAdaptor {
    List<IBattle> Teammates { get; }
  }
}

