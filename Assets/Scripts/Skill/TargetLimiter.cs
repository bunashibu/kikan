using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLimiter : MonoBehaviour {
  void Awake() {
    _targetList = new Dictionary<GameObject, int>();
  }

  public bool Check(GameObject target) {
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

