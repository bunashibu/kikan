using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IMediator {
    void OnNotify(Notification notification, object[] args);
    PhotonView PhotonView { get; }
  }
}

