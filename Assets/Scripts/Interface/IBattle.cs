using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattle : IMediator {
    int KillExp { get; }
    int KillGold { get; }
    int DamageSkinId { get; }
  }
}

