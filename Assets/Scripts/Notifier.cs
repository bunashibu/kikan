using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Notifier {
    public Notifier() {
      _observerList = new List<IObserver>();
    }

    public Notifier(IObserver[] observers) : this() {
      foreach (IObserver observer in observers)
        Add(observer);
    }

    public void Notify(Notification notification, params object[] args) {
      foreach (IObserver observer in _observerList)
        observer.OnNotify(notification, args);
    }

    public void Add(IObserver observer) {
      _observerList.Add(observer);
    }

    private List<IObserver> _observerList;
  }
}

