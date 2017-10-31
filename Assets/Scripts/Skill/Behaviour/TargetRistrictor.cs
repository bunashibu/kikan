using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor {
    public TargetRistrictor(int targetNum, int dupHitNum) {
      _targetList = new Dictionary<IBattle, int>();
      _targetNum = targetNum;
      _dupHitNum = dupHitNum;
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

    private Dictionary<IBattle, int> _targetList;
    private int _targetNum;
    private int _dupHitNum;
  }
}

