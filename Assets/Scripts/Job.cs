using UnityEngine;
using System;
using System.Collections;

public class Job : MonoBehaviour {
  protected IEnumerator DelayMethod(float waitTime, Action action) {
    yield return new WaitForSeconds(waitTime);
    action();
  }

  [SerializeField] protected Sprite _actionNormal;
  [SerializeField] protected Sprite _actionX;
  [SerializeField] protected Sprite _actionShift;
}
