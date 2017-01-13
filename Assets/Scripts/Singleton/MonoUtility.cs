using UnityEngine;
using System;
using System.Collections;

public class MonoUtility : SingletonMonoBehaviour<MonoUtility> {
  public void DelayOneFrame(Action action) {
    StartCoroutine(ImplDelayOneFrame(action));
  }

  public void DelaySec(float sec, Action action) {
    StartCoroutine(ImplDelaySec(sec, action));
  }

  private IEnumerator ImplDelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  private IEnumerator ImplDelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }
}

