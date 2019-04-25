using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  // SkillReference manages only own instantiated skill.
  // That means that each clients manage own skills.
  public class SkillReference : SingletonMonoBehaviour<SkillReference> {
    void Awake() {
      _existOwnIdList = new List<int>();
      _dictionary = new Dictionary<int, SkillCoroutine>();
    }

    public void Register(int viewID) {
      if (!_existOwnIdList.Contains(viewID))
        _existOwnIdList.Add(viewID);
    }

    public void Register(int viewID, float sec, Action action) {
      Register(viewID);

      _dictionary[viewID] = new SkillCoroutine(action, MonoUtility.Instance.ImplDelaySec(sec, action));
      StartCoroutine(_dictionary[viewID].Coroutine);
    }

    public void Remove(int viewID) {
      if (_existOwnIdList.Contains(viewID))
        _existOwnIdList.Remove(viewID);

      if (_dictionary.ContainsKey(viewID))
        _dictionary.Remove(viewID);
    }

    public void DeleteAll() {
      foreach (int viewID in _existOwnIdList) {
        var skillObj = PhotonView.Find(viewID);
        if (skillObj == null)
          continue;

        PhotonNetwork.Destroy(skillObj);
      }

      foreach (SkillCoroutine skillCoroutine in _dictionary.Values)
        StopCoroutine(skillCoroutine.Coroutine);

      _existOwnIdList.Clear();
      _dictionary.Clear();
    }

    // 1. Client's all skill is observed by _existOwnIdList.
    // 2. But when a skill uses some DelayFunction(like enhance status), the coroutine and action
    //    that the skill uses are also observed by _dictionary in addition.
    private List<int> _existOwnIdList;
    private Dictionary<int, SkillCoroutine> _dictionary;
  }

  public class SkillCoroutine {
    public SkillCoroutine(Action action, IEnumerator coroutine) {
      Action = action;
      Coroutine = coroutine;
    }

    public Action Action;
    public IEnumerator Coroutine;
  }
}

