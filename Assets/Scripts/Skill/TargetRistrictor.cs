using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetRistrictor : MonoBehaviour {
    void Awake() {
      _targetList = new Dictionary<GameObject, int>();
    }

    public bool ShouldPass(GameObject target, int team) {
      return IsOtherTeam(target, team) && !IsMaxDupHit(target);
    }

    private bool IsOtherTeam(GameObject target, int team) {
      int targetTeam = (int)target.GetComponent<PhotonView>().owner.CustomProperties["Team"];

      if (targetTeam == team)
        return false;
      else
        return true;
    }

    private bool IsMaxDupHit(GameObject target) {
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

    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;
    private Dictionary<GameObject, int> _targetList;
  }
}

