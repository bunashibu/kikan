using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class TargetChecker {
    public TargetChecker(int maxSimulHit, int maxSustainHit = 0) {
      _targetRistrictor = new TargetRistrictor(maxSimulHit, maxSustainHit);
    }

    public bool IsSameTeam(GameObject targetObj, GameObject skillUserObj) {
      var target = targetObj.GetComponent<Player>();
      var skillUser = skillUserObj.GetComponent<Player>();

      if (target == null || skillUser == null)
        return false;

      return (target.PlayerInfo.Team == skillUser.PlayerInfo.Team);
    }

    public bool IsNotSameTeam(GameObject targetObj, GameObject skillUserObj) {
      return !IsSameTeam(targetObj, skillUserObj);
    }

    public bool IsMaxSimulHit(GameObject targetObj) {
      return false;
    }

    public bool IsMaxSustainHit(GameObject targetObj) {
      var target = targetObj.GetComponent<IOnAttacked>();

      if (target == null)
        return true;

      return _targetRistrictor.ShouldRistrict(target);
    }

    public bool IsNeedInterval(GameObject target) {
      return false;
    }

    private TargetRistrictor _targetRistrictor;
  }
}

