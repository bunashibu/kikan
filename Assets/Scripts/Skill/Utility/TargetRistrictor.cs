using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor {
    public TargetRistrictor(int maxTargetCount, int maxHitCount) {
      _targetDictionary = new Dictionary<IOnAttacked, int>();
      _maxTargetCount = maxTargetCount;
      _maxHitCount = maxHitCount;
      _isMaxTargetCount = IsMaxTargetCount();
    }

    public bool ShouldRistrict(IOnAttacked target) {
      return _isMaxTargetCount() || IsMaxHitCount(target);
    }

    private Func<bool> IsMaxTargetCount() {
      int i = 0;

      return () => {
        i += 1;
        if (i > _maxTargetCount)
          return true;

        return false;
      };
    }

    private bool IsMaxHitCount(IOnAttacked target) {
      if (!_targetDictionary.ContainsKey(target)) {
        _targetDictionary[target] = 0;
        return false;
      }

      if (_targetDictionary[target] < _maxHitCount) {
        _targetDictionary[target] += 1;
        return false;
      }
      else
        return true;
    }

    private Dictionary<IOnAttacked, int> _targetDictionary;
    private Func<bool> _isMaxTargetCount;
    private int _maxTargetCount;
    private int _maxHitCount;
  }
}

