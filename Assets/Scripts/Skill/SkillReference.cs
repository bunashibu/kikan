using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  // SkillReference manages only own instantiated skill.
  // That means that each clients manage own skills.
  public class SkillReference : SingletonMonoBehaviour<SkillReference> {
    void Awake() {
      _existOwnSkillList = new List<Skill>();
      _dictionary = new Dictionary<Skill, SkillCoroutine>();
    }

    public void Register(Skill skill) {
      if (!_existOwnSkillList.Contains(skill))
        _existOwnSkillList.Add(skill);
    }

    public void Register(Skill skill, float sec, Action action) {
      Register(skill);

      _dictionary[skill] = new SkillCoroutine(action, MonoUtility.Instance.ImplDelaySec(sec, action));
      StartCoroutine(_dictionary[skill].Coroutine);
    }

    public void Remove(Skill skill) {
      if (_existOwnSkillList.Contains(skill))
        _existOwnSkillList.Remove(skill);

      if (_dictionary.ContainsKey(skill))
        _dictionary.Remove(skill);
    }

    public void DeleteAll() {
      foreach (Skill skill in _existOwnSkillList) {
        if (skill == null)
          continue;

        PhotonNetwork.Destroy(skill.gameObject);
      }

      foreach (SkillCoroutine skillCoroutine in _dictionary.Values)
        StopCoroutine(skillCoroutine.Coroutine);

      _existOwnSkillList = new List<Skill>();
      _dictionary = new Dictionary<Skill, SkillCoroutine>();
    }

    // 1. Client's all skill is observed by _existOwnSkillList.
    // 2. But when a skill uses some DelayFunction(like enhance status), the coroutine and action
    //    that the skill uses are also observed by _dictionary in addition.
    private List<Skill> _existOwnSkillList;
    private Dictionary<Skill, SkillCoroutine> _dictionary;
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

