using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Hp : Gauge<int> {
    public Hp(params IObserver[] observers) {
      Notifier = new Notifier(observers);
    }

    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.GiveInitialHp:
          Cur = (int)args[0];
          Min = 0;
          Max = (int)args[0];

          Notifier.Notify(Notification.HpUpdated, Cur, Max);
          break;
        default:
          break;
      }
    }

    public override void Add(int quantity) { // to be protected
      Cur += quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpUpdated, Cur, Max);
    }

    public override void Subtract(int quantity) { // to be protected
      Cur -= quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpUpdated, Cur, Max);
    }

    private void AdjustBoundary() {
      if (Cur < Min)
        Cur = Min;
      if (Cur > Max)
        Cur = Max;
    }

    public Notifier Notifier { get; private set; }

    public void UpdateView() {}
    public void FullRecover() {}
    public void AttachHudBar(Bar hudBar) {}
    public void UpdateMaxHp() {}
    public void ForceSync(int cur, int max) {}
    public void ForceSyncCur(int cur) {}
    public void ForceSyncMax(int max) {}
    public void ForceSyncUpdateView() {}
  }
}

