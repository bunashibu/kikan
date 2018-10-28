using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Duplexer {
    public Duplexer() {
      _notifier = new Notifier();
    }

    public void AddListener(Action<Notification, object[]> onNotify) {
      _notifier.AddListener(onNotify);
    }

    public abstract void OnNotify(Notification notification, object[] args);

    protected Notifier _notifier;
  }
}

