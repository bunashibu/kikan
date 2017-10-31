using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor : MonoBehaviour {
    void Awake() {
      _targetList = new Dictionary<IBattle, int>();
    }

    public bool ShouldRistrict(IBattle target) {
      return IsMaxDupHit(target);
    }

    private bool IsMaxDupHit(IBattle target) {
      if (!_targetList.ContainsKey(target)) {
        _targetList[target] = 0;
        return false;
      }

      if (_targetList[target] < _dupHitNum) {
        _targetList[target] += 1;
        return false;
      } else {
        return true;
      }
    }

    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;
    private Dictionary<IBattle, int> _targetList;
  }
}

