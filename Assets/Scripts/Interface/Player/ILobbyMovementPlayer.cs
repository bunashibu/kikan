using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface ILobbyMovementPlayer {
    GameObject  gameObject { get; }
    Transform   transform  { get; }
    Rigidbody2D Rigid      { get; }
  }
}

