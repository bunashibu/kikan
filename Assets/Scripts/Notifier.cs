using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Notifier {
    public Notifier() {
      _receiveList = new List<Action<Notification, object[]>>();
    }

    public Notifier(params Action<Notification, object[]>[] receives) : this() {
      foreach (var receive in receives)
        Add(receive);
    }

    public void Send(Notification notification, params object[] args) {
      foreach (var receive in _receiveList)
        receive(notification, args);
    }

    public void Add(Action<Notification, object[]> receive) {
      _receiveList.Add(receive);
    }

    private List<Action<Notification, object[]>> _receiveList;
  }
}

