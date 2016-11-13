using UnityEngine;
using System;
using System.Collections;

public class SkillSystem : MonoBehaviour {
  void Update() {
    foreach (var skill in _skills)
      skill.Activate();
  }

  [SerializeField] private Skill[] _skills;
}

