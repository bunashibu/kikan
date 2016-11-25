using UnityEngine;
using System;
using System.Collections;

public class Skill : MonoBehaviour {
  void Update() {
    _behaviour.Behave();
  }

  [SerializeField] private SkillBehaviour _behaviour;
}

