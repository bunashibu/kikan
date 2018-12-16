using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IKillRewardTaker {
    Exp  Exp  { get; }
    Gold Gold { get; }
    List<IKillRewardTaker> RewardTeammates { get; }
  }
}

