using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IEnemyLocationJudger {
    void InitializeFootJudge(Collider2D footCollider);

    bool IsGround { get; }
  }
}

