using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HpBar : MonoBehaviour, IObserver {
    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.HpAdd:
          _view.UpdateView((int)args[0], (int)args[1]); // cur, max
          break;
        case Notification.HpSubtract:
          _view.UpdateView((int)args[0], (int)args[1]); // cur, max
          break;
        default:
          break;
      }
    }

    [SerializeField] private BarView _view;
  }
}

