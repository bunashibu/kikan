using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IPhoton {
    GameObject gameObject { get; }
    PhotonView PhotonView { get; }
  }
}

