using UnityEngine;
using System;
using System.Collections;

public class Utility : MonoBehaviour {
  void Awake() {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);
  }

  public IEnumerator DelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  public IEnumerator DelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }

  public static Utility instance = null;
}
