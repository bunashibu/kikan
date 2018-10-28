using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyHp : Gauge {
    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.Initialize:
          Assert.IsTrue(args.Length == 1);

          int initialHp = (int)args[0];
          Cur = initialHp;
          Max = initialHp;

          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 4);

          int damage = (int)args[1];
          Subtract(damage);

          var attacker = ((GameObject)args[0]).GetComponent<IBattle>();
          Assert.IsNotNull(attacker);

          if (attacker.PhotonView.isMine)
            _notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }

    // NOTE: Obsolete methods. Just exists because of backward compatibilty.
    public void UpdateView() {}
    public void UpdateView(PhotonPlayer skillOwner) {}
    public void FullRecover() {}
    public void AttachHudBar(Bar hudBar) {}
    public void UpdateMaxHp() {}
    public void ForceSync(int cur, int max) {}
    public void ForceSyncCur(int cur) {}
    public void ForceSyncMax(int max) {}
    public void ForceSyncUpdateView() {}
    public void ForceSyncUpdateView(PhotonPlayer skillOwner) {}
  }
}

