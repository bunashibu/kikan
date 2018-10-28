using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Notifier {
    public Notifier() {
      _onNotifyList = new List<Action<Notification, object[]>>();
    }

    public Notifier(params Action<Notification, object[]>[] onNotifyList) : this() {
      foreach (var onNotify in onNotifyList)
        AddListener(onNotify);
    }

    public void Notify(Notification notification, params object[] args) {
      foreach (var onNotify in _onNotifyList)
        onNotify(notification, args);
    }

    public void AddListener(Action<Notification, object[]> onNotify) {
      _onNotifyList.Add(onNotify);
    }

    public void RemoveAllListeners() {
      _onNotifyList = new List<Action<Notification, object[]>>();
    }

    private List<Action<Notification, object[]>> _onNotifyList;
  }
}

