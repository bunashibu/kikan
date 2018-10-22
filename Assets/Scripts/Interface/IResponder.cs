using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IResponder {
    Notifier Notifier { get; }
    void OnNotify(Notification notification, object[] args);
  }
}

