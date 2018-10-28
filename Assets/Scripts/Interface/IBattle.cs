using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattle : IMediatorAdaptor {
    int KillExp      { get; }
    int KillGold     { get; }
    int DamageSkinId { get; }
    int Power        { get; }
    int Critical     { get; }
  }
}

