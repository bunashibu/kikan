using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  public void Init(Health health, BattleSceneManager manager) {
    _health = health;

    _bar = Instantiate(_bar) as HealthBar;
    _bar.transform.SetParent(manager.hud.transform, false);
  }

  public void IsHealed(int quantity) {
    _health.Plus(quantity);
    Show();
  }

  public void IsDamaged(int quantity) {
    IsHealed(-quantity);

    if (_health.IsDead())
      Die();
  }

  public void Show() {
    _bar.Show(_health.Get(), _health.GetMax());
  }

  public void Die() {
    _anim.SetBool("Die", true);
  }

  [SerializeField] private HealthBar _bar;
  [SerializeField] private Animator _anim;
  private Health _health;
}
