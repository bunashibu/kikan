using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public static class TeamChecker {
    public static bool IsSameTeam(GameObject targetObj, GameObject skillUserObj) {
      var target = targetObj.GetComponent<Player>();
      var skillUser = skillUserObj.GetComponent<Player>();

      if (target == null || skillUser == null)
        return false;

      return (target.PlayerInfo.Team == skillUser.PlayerInfo.Team);
    }

    public static bool IsNotSameTeam(GameObject targetObj, GameObject skillUserObj) {
      return !IsSameTeam(targetObj, skillUserObj);
    }
  }
}

