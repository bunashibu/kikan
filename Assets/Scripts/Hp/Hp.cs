using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Hp {
    public Hp() {
      Notifier = new Notifier();
    }

    public abstract void OnNotify(Notification notification, object[] args);

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

