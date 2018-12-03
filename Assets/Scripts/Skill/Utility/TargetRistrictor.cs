using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor {
    public TargetRistrictor(int targetNum, int dupHitNum) {
      _targetDictionary = new Dictionary<IOnAttacked, int>();
      _targetNum = targetNum;
      _dupHitNum = dupHitNum;
      _isMaxTargetHit = IsMaxTargetHit();
    }

    public bool ShouldRistrict(IOnAttacked target) {
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

    private bool IsMaxDupHit(IOnAttacked target) {
      if (!_targetDictionary.ContainsKey(target)) {
        _targetDictionary[target] = 0;
        return false;
      }

      if (_targetDictionary[target] < _dupHitNum) {
        _targetDictionary[target] += 1;
        return false;
      }
      else
        return true;
    }

    private Dictionary<IOnAttacked, int> _targetDictionary;
    private Func<bool> _isMaxTargetHit;
    private int _targetNum;
    private int _dupHitNum;
  }
}

