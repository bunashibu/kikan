using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface ICorePlayer : IPhotonBehaviour {
    Core Core { get; }
    Gold Gold { get; }
    PlayerStreamSynchronizer Synchronizer { get; }
  }
}

