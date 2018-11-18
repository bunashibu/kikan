using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface ICorePlayer : IMonoBehaviour, IPhoton {
    Core Core { get; }
    Gold Gold { get; }
    int CurGold { get; }
  }
}

