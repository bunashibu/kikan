using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor {
    public TargetRistrictor(int targetNum, int dupHitNum) {
      _targetList = new Dictionary<IBattle, int>();
      _targetNum = targetNum;
      _dupHitNum = dupHitNum;
      _isMaxTargetHit = IsMaxTargetHit();
    }

    public bool ShouldRistrict(IBattle target) {
      return _isMaxTargetHit() || IsMaxDupHit(target);
    }

    private Func<bool> IsMaxTargetHit() {
      int i = 0;

      return () => {
        i += 1;
        if (i > _targetNum)
          return true;

        return false;
      };
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
    private Func<bool> _isMaxTargetHit;
    private int _targetNum;
    private int _dupHitNum;
  }
}

