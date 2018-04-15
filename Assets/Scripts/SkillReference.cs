using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillReference : SingletonMonoBehaviour<SkillReference> {
    void Awake() {
      _existOwnSkillList = new List<Skill>();
    }

    public void Register(Skill skill) {
      _existOwnSkillList.Add(skill);
    }

    public void Remove(Skill skill) {
      _existOwnSkillList.Remove(skill);
    }

    public void DeleteAll() {
      foreach (Skill skill in _existOwnSkillList) {
        PhotonNetwork.Destroy(skill.gameObject);
      }

      _existOwnSkillList = new List<Skill>();
    }

    private List<Skill> _existOwnSkillList;
  }
}

