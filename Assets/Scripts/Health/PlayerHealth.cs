using UnityEngine;
using System.Collections;

public class PlayerHealth : Photon.MonoBehaviour {
  public void Init(Health health, Bar hudBar) {
    _health = health;
    _hudBar = hudBar;

    if (photonView.isMine)
      _hiddenBar.gameObject.SetActive(false);
  }

  public void IsHealed(int quantity) {
    _health.Plus(quantity);
    Show();
  }

  public void IsDamaged(int quantity) {
    IsHealed(-quantity);

    if (_health.Dead)
      Die();
  }

  public void Show() {
    _hudBar.Show(_health.Cur, _health.Max);
  }

  // called by other players
  public void ShowHidden() {
    _hiddenBar.Show(_health.Cur, _health.Max);
  }

  public void Die() {
    _anim.SetBool("Die", true);
  }

  [SerializeField] private Animator _anim;
  [SerializeField] private Bar _hiddenBar;
  private Health _health;
  private Bar _hudBar;
}

