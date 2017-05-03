using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
  public void Init(int life, Bar bar) {
    //Init(life, life);
    _bar = bar;
  }

  public void Show() {
    _bar.Show(Cur, Max);
  }

  public override void Die() {
    base.Die();
    _anim.SetBool("Die", true);
  }

  [SerializeField] private Animator _anim;
  private Bar _bar;
}

