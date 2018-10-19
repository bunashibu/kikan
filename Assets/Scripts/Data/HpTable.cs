using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class HpTable {
    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.PlayerInstantiated:
          //Notifier.Notify(Notification.GiveInitialHp, Data[0]);
          break;
        default:
          break;
      }
    }
  }
}

