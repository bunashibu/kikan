using UnityEngine;
using System;
using System.Collections;

public class MonoUtility : SingletonMonoBehaviour<MonoUtility> {
  public IEnumerator DelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  public IEnumerator DelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }
}

