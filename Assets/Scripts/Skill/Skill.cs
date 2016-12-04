using UnityEngine;
using System;
using System.Collections;

public class Skill : MonoBehaviour {
  void Start() {
    _anim.SetBool(_animName, true);
  }

  void Update() {
    _behaviour.Behave();
  }

  [SerializeField] private SkillBehaviour _behaviour;
  [SerializeField] private Animator _anim;
  [SerializeField] private string _animName;
}

