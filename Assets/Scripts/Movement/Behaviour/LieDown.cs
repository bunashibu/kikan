using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LieDown {
    public void Lie(BoxCollider2D collider) {
      collider.offset = new Vector2(0.0f, -0.2f);
      collider.size = new Vector2(0.9f, 0.6f);
    }

    public void Stand(BoxCollider2D collider) {
      collider.offset = new Vector2(0.0f, -0.05f);
      collider.size = new Vector2(0.6f, 0.9f);
    }

    public bool CanUse { get; private set; }
  }
}

