using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IKillRewardGiver {
    int KillExp  { get; }
    int KillGold { get; }
  }
}

