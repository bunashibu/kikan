using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class WarriorCtrl : Skill {
    void Awake() {
      _animator = GetComponent<Animator>();
    }

    void Start() {
      transform.parent = _skillUserObj.transform;
    }

    private Animator _animator;
  }
}

