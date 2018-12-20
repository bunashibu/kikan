using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetChaseSystem : MonoBehaviour {
    void Update() {
    }

    public void RegisterTarget(Transform targetTrans) {
      _targetTrans = targetTrans;
    }

    private Transform _targetTrans;
  }
}

