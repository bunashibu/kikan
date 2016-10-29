using UnityEngine;
using System;
using System.Collections;

public class Job : MonoBehaviour {
  protected IEnumerator DelayMethod(float waitTime, Action action) {
    yield return new WaitForSeconds(waitTime);
    action();
  }

  public void DecreaseLife(int damage) {
    _life -= damage;
    if (_life < 0)
      _life = 0;
  }

  [SerializeField] protected Sprite _actionNormal;
  [SerializeField] protected Sprite _actionX;
  [SerializeField] protected Sprite _actionShift;
  [SerializeField] protected int _life;
}
