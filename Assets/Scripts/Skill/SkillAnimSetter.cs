using UnityEngine;
using System.Collections;

public class SkillAnimSetter : MonoBehaviour {
  void Start() {
    _anim.SetBool(_animName, true);
  }

  [SerializeField] private Animator _anim;
  [SerializeField] private string _animName;
}

