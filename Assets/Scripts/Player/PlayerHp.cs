using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerHp : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 4);

          int initialHp = (int)args[0];
          Cur = initialHp;
          Max = initialHp;

          _notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 4);

          int damage = (int)args[1];
          Subtract(damage);

          _notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        case Notification.LevelUp:
          Assert.IsTrue(args.Length == 2);

          int maxHp = (int)args[0];
          Max = maxHp;

          _notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }

    // NOTE: Obsolete methods. Just exists because of backward compatibilty.
    public void UpdateView() {}
    public void UpdateView(PhotonPlayer skillOwner) {}
    public void FullRecover() { Cur = Max; }
    public void AttachHudBar(Bar hudBar) {}
    public void UpdateMaxHp() {}
    public void ForceSync(int cur, int max) {}
    public void ForceSyncCur(int cur) {}
    public void ForceSyncMax(int max) {}
    public void ForceSyncUpdateView() {}
    public void ForceSyncUpdateView(PhotonPlayer skillOwner) {}
  }
}

