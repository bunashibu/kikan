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

  public void DelayUntil(Func<bool> condition, Action action) {
    StartCoroutine(ImplDelayUntil(condition, action));
  }

  private IEnumerator ImplDelayOneFrame(Action action) {
    yield return new WaitForEndOfFrame();
    action();
  }

  private IEnumerator ImplDelaySec(float sec, Action action) {
    yield return new WaitForSeconds(sec);
    action();
  }

  private IEnumerator ImplDelayUntil(Func<bool> condition, Action action) {
    yield return new WaitUntil(condition);
    action();
  }
}

