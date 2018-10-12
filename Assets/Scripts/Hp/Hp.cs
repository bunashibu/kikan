using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Hp : Gauge<int>, IObserver {
    public Hp(params IObserver[] observers) {
      Notifier = new Notifier(observers);
      Notifier.Notify(Notification.HpInit);
    }

    public override void Add(int quantity) {
      Cur += quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpAdd, Cur, Max);
    }

    public override void Subtract(int quantity) {
      Cur -= quantity;
      AdjustBoundary();

      Notifier.Notify(Notification.HpSubtract, Cur, Max);
    }

    private void AdjustBoundary() {
      if (Cur < Min)
        Cur = Min;
      if (Cur > Max)
        Cur = Max;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.HpInit:
          Cur = (int)args[0];
          Min = 0;
          Max = (int)args[0];
          break;
        default:
          break;
      }
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

