using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetChecker {
    public TargetChecker(GameObject skillUserObj, int targetNum, int dupHitNum = 0) {
      _targetRistrictor = new TargetRistrictor(targetNum, dupHitNum);
      _skillUserObj     = skillUserObj;
    }

    public bool IsAttackTarget(Collider2D collider) {
      var targetObj = collider.gameObject;

      if (targetObj == _skillUserObj)
        return false;

      switch (targetObj.tag) {
        case "Player":
          if (IsDupHit(targetObj) || IsSameTeam(targetObj))
            return false;

          break;
        case "Enemy":
          if (IsDupHit(targetObj))
            return false;

          break;
        default:
          return false;
      }

      return true;
    }

    private bool IsDupHit(GameObject targetObj) {
      IBattle target = targetObj.GetComponent<IBattle>();

      return _targetRistrictor.ShouldRistrict(target);
    }

    private bool IsSameTeam(GameObject targetObj) {
    // target.PlayerInfo.Team == skillUser.PlayerInfo.Team)
      return false;
    }

    private TargetRistrictor _targetRistrictor;
    private GameObject       _skillUserObj;
  }
}

