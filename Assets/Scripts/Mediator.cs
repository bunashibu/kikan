using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Mediator {
    public Mediator() {
      _notifier = new Notifier();
    }

    public void AddListener(Action<Notification, object[]> onNotify) {
      _notifier.AddListener(onNotify);
    }

    protected Notifier _notifier;
  }
}

