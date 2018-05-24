using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FPSManager : SingletonMonoBehaviour<FPSManager> {
    void Awake() {
      Application.targetFrameRate = 60;
    }
  }
}

