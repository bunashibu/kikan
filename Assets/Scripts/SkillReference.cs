using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillReference : MonoBehaviour {
    void Awake() {
      _existSkillList = new List<Skill>();
    }

    public void Register(Skill skill) {
      _existSkillList.Add(skill);
    }

    public void Remove(Skill skill) {
      _existSkillList.Remove(skill);
    }

    public void DeleteAll() {
      foreach (Skill skill in _existSkillList) {
        Destroy(skill.gameObject);
      }

      _existSkillList = new List<Skill>();
    }

    private List<Skill> _existSkillList;
  }
}

