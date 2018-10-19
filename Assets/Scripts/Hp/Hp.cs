using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class Hp : IListener {
    public Hp() {
      Notifier = new Notifier();
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.InitializeHp:
          Assert.IsTrue(args.Length == 1);

          Cur = (int)args[0];
          Max = (int)args[0];

          Notifier.Notify(Notification.HpUpdated, Cur, Max);
          break;
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 3);

          int damage = (int)args[0];
          Subtract(damage);

          Notifier.Notify(Notification.HpUpdated, Cur, Max);

          break;
        default:
          break;
      }
    }

    public void Add(int quantity) { // to be protected
      Cur += quantity;
      AdjustBoundary();
    }

    public void Subtract(int quantity) { // to be protected
      Cur -= quantity;
      AdjustBoundary();
    }

    private void AdjustBoundary() {
      if (Cur < Min)
        Cur = Min;
      if (Cur > Max)
        Cur = Max;
    }

    public Notifier Notifier { get; private set; }

    public int Cur { get; protected set; }
    public int Min { get { return 0; }   }
    public int Max { get; protected set; }

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

