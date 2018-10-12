using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class HpTable : DataTable, IObserver {
    public HpTable() {
      Notifier = new Notifier();
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.HpInit:
          Notifier.Notify(Notification.HpInit, Data[0]);
          break;
        default:
          break;
      }
    }

    public Notifier Notifier { get; private set; }
  }
}

