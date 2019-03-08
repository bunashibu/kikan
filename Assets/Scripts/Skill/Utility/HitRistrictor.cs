using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HitRistrictor {
    public HitRistrictor(HitInfo hitInfo) {
      _hitInfo = hitInfo;
      _curState = new Dictionary<IOnAttacked, TargetHitState>();
    }

    public bool ShouldRistrict(GameObject targetObj) {
      var target = targetObj.GetComponent<IOnAttacked>();

      if (target == null)
        return true;

      bool shouldRistrict = IsMaxTargetCount() || IsMaxHitCount(target) || IsNeedInterval(target);

      if (shouldRistrict)
        return true;
      else {
        UpdateDictionary(target);
        return false;
      }
    }

    private bool IsMaxTargetCount() {
      var curCount = _curState.Values.Where(state => Time.time - state.Timestamp < _hitInfo.Interval).Count();

      return (curCount >= _hitInfo.MaxTargetCount);
    }

    private bool IsMaxHitCount(IOnAttacked target) {
      if (IsNewTarget(target))
        return false;

      if (_curState[target].HitCount < _hitInfo.MaxHitCount)
        return false;

      return true;
    }

    private bool IsNeedInterval(IOnAttacked target) {
      if (IsNewTarget(target))
        return false;

      if ((Time.time - _curState[target].Timestamp) >= _hitInfo.Interval)
        return false;

      return true;
    }

    private bool IsNewTarget(IOnAttacked target) {
      if (_curState.ContainsKey(target))
        return false;
      else
        return true;
    }

    private void UpdateDictionary(IOnAttacked target) {
      if (IsNewTarget(target)) {
        var state = new TargetHitState();
        state.HitCount = 1;
        state.Timestamp = Time.time;

        _curState.Add(target, state);
      }
      else {
        _curState[target].HitCount += 1;
        _curState[target].Timestamp = Time.time;
      }
    }

    private Dictionary<IOnAttacked, TargetHitState> _curState;
    private HitInfo _hitInfo;
  }

  public class TargetHitState {
    public int HitCount;
    public float Timestamp;
  }
}

