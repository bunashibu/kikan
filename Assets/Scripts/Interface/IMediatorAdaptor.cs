using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IMediatorAdaptor {
    void OnNotify(Notification notification, object[] args);
    PhotonView PhotonView { get; }
  }
}

