using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

    public bool IsSameTeam(GameObject targetObj, GameObject skillUserObj) {
      var target = targetObj.GetComponent<Player>();
      var skillUser = skillUserObj.GetComponent<Player>();

      if (target == null || skillUser == null)
        return false;

      return (target.PlayerInfo.Team == skillUser.PlayerInfo.Team);
    }

    private bool IsDupHit(GameObject targetObj) {
      var target = targetObj.GetComponent<IOnAttacked>();
      Assert.IsNotNull(target);

      return _targetRistrictor.ShouldRistrict(target);
    }

    private TargetRistrictor _targetRistrictor;
  }
}

