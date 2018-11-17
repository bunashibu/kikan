using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StunEffect : MonoBehaviour {
    void Awake() {
      transform.Translate(Vector2.up * 0.5f);
    }
  }
}

