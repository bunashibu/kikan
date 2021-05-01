﻿using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bunashibu.Kikan {
  public class MonoUtility : SingletonMonoBehaviour<MonoUtility> {
    protected override void Awake() {
      _coroutineDictionary = new Dictionary<string, IEnumerator>();
    }

    public void DelayOneFrame(Action action) {
      StartCoroutine(ImplDelayOneFrame(action));
    }

    public void DelaySec(float sec, Action action) {
      StartCoroutine(ImplDelaySec(sec, action));
    }

    public void DelayUntil(Func<bool> condition, Action action) {
      StartCoroutine(ImplDelayUntil(condition, action));
    }

    // Memo: 1, This func should not be here.
    //       2, Something that managing delay func class should be created.
    public void OverwritableDelaySec(float sec, string keyName, Action action) {
      if (_coroutineDictionary.ContainsKey(keyName))
        StopCoroutine(_coroutineDictionary[keyName]);

      _coroutineDictionary[keyName] = ImplDelaySec(sec, action);
      StartCoroutine(_coroutineDictionary[keyName]);
    }

    public void StoppableDelaySec(float sec, string keyName, Action action) {
      _coroutineDictionary[keyName] = ImplDelaySec(sec, action);
      StartCoroutine(_coroutineDictionary[keyName]);
    }

    public void Stop(string keyName) {
      if (!_coroutineDictionary.ContainsKey(keyName))
        return;

      StopCoroutine(_coroutineDictionary[keyName]);
      _coroutineDictionary.Remove(keyName);
    }

    public void StopAll() {
      foreach (IEnumerator coroutine in _coroutineDictionary.Values)
        StopCoroutine(coroutine);

      _coroutineDictionary = new Dictionary<string, IEnumerator>();
    }

    public static List<T> ToList<T>(T[] ary) {
      var list = new List<T>();

      if (ary != null)
        list.AddRange(ary);

      return list;
    }

    public IEnumerator ImplDelayOneFrame(Action action) {
      yield return new WaitForEndOfFrame();
      action();
    }

    public IEnumerator ImplDelaySec(float sec, Action action) {
      yield return new WaitForSeconds(sec);
      action();
    }

    public IEnumerator ImplDelayUntil(Func<bool> condition, Action action) {
      yield return new WaitUntil(condition);
      action();
    }

    private Dictionary<string, IEnumerator> _coroutineDictionary;
  }
}
