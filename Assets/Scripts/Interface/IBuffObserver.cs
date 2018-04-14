using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBuffObserver {
    bool ShouldSync(string key);
    void SyncBuff();
    void SyncStun();
  }
}

