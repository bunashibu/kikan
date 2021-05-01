using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FPSManager : SingletonMonoBehaviour<FPSManager> {
    protected override void Awake() {
      Application.targetFrameRate = 60;
    }
  }
}
