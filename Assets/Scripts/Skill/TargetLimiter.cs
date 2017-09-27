using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TargetLimiter : MonoBehaviour {
    void Awake() {
      _targetList = new Dictionary<GameObject, int>();
    }
  
    public bool Check(GameObject target, int team) {
      bool isOtherTeam = OtherTeamCheck(target, team);
      bool isNotDupHit = DupHitCheck(target);
  
      return isOtherTeam && isNotDupHit;
    }
  
    private bool OtherTeamCheck(GameObject target, int team) {
      int targetTeam = (int)target.GetComponent<PhotonView>().owner.CustomProperties["Team"];
  
      if (targetTeam == team)
        return false;
      else
        return true;
    }
  
    private bool DupHitCheck(GameObject target) {
      if (_targetList.ContainsKey(target))
        return SecondOrLaterTimeCheck(target);
      else
        return FirstTimeCheck(target);
    }
  
    private bool FirstTimeCheck(GameObject target) {
      _targetList[target] = 0;
      return true;
    }
  
    private bool SecondOrLaterTimeCheck(GameObject target) {
      if (_targetList[target] < _dupHitNum) {
        _targetList[target] += 1;
        return true;
      } else
        return false;
    }
  
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;
    private Dictionary<GameObject, int> _targetList;
  }
}

