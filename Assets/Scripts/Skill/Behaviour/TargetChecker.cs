using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetChecker {
    public TargetChecker(int targetNum, int dupHitNum = 0) {
      _targetRistrictor = new TargetRistrictor(targetNum, dupHitNum);
    }

    public bool IsAttackTarget(Collider2D collider, GameObject skillUserObj) {
      var targetObj = collider.gameObject;

      if (targetObj == skillUserObj)
        return false;

      switch (targetObj.tag) {
        case "Player":
          if (IsDupHit(targetObj) || IsSameTeam(targetObj, skillUserObj))
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

    private bool IsSameTeam(GameObject targetObj, GameObject skillUserObj) {
      var target    = targetObj.GetComponent<BattlePlayer>();
      var skillUser = skillUserObj.GetComponent<BattlePlayer>();

      return (target.PlayerInfo.Team == skillUser.PlayerInfo.Team);
    }

    private TargetRistrictor _targetRistrictor;
  }
}

