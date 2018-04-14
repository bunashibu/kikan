using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class BuffState {
    public BuffState(IBuffObserver observer) {
      _observer = observer;
    }

    public void Reset() {
      // Add slow, heavy
      MonoUtility.Instance.Stop("BuffStun");

      Stun = false;
      Slow = false;
      Heavy = false;
      _observer.SyncBuff();
    }

    public void ToBeStun(float sec) {
      Stun = true;
      _observer.SyncStun();

      MonoUtility.Instance.StoppableDelaySec(sec, "BuffStun", () => {
        Stun = false;
        _observer.SyncStun();
      });
    }

    /*                                                            *
     * INFO: ForceSyncXXX method must be called ONLY by Observer. *
     *       Otherwise it breaks encapsulation.                   *
     *                                                            */
    public void ForceSync(bool stun, bool slow, bool heavy) {
      Assert.IsTrue(_observer.ShouldSync("Buff"));
      Stun = stun;
      Slow = slow;
      Heavy = heavy;
    }

    public void ForceSyncStun(bool stun) {
      Assert.IsTrue(_observer.ShouldSync("BuffStun"));
      Stun = stun;
    }

    public bool Stun  { get; private set; }
    public bool Slow  { get; private set; }
    public bool Heavy { get; private set; }

    private IBuffObserver _observer;
  }
}

