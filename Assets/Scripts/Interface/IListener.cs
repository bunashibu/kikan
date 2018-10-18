using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IListener {
    void OnNotify(Notification notification, object[] args);
  }
}

