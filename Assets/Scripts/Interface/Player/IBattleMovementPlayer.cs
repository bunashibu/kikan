using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattleMovementPlayer : IMonoBehaviour {
    Rigidbody2D Rigid { get; }
    Core        Core  { get; }
  }
}

