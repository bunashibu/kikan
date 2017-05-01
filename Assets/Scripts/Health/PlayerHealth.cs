using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {
  public void Init(int life, Bar hudBar) {
    Init(life, life);

    _hudBar = hudBar;

    if (photonView.isMine)
      _worldBar.gameObject.SetActive(false);
  }

  public void Show() {
    if (photonView.isMine)
      _hudBar.Show(Cur, Max);
    else
      _worldBar.Show(Cur, Max);
  }

  public override void Die() {
    base.Die();
    _anim.SetBool("Die", true);
  }

  [SerializeField] private Animator _anim;
  [SerializeField] private Bar _worldBar;
  private Bar _hudBar;
}

