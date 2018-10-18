using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Notifier {
    public Notifier() {
      _listenerList = new List<IListener>();
    }

    public Notifier(params IListener[] listeners) : this() {
      foreach (IListener listener in listeners)
        Add(listener);
    }

    public void Notify(Notification notification, params object[] args) {
      foreach (IListener listener in _listenerList)
        listener.OnNotify(notification, args);
    }

    public void Add(IListener listener) {
      _listenerList.Add(listener);
    }

    private List<IListener> _listenerList;
  }
}

