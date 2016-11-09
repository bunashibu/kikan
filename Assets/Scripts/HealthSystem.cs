using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  public void Init(int life, int maxLife, BattleSceneManager manager) {
    _health.Init(life, maxLife);
    bar = Instantiate(bar) as HealthBar;
    bar.transform.SetParent(manager.hud.transform, false);
  }

  public void IsHealed(int quantity) {
    _health.Plus(quantity);
    Show();
  }

  public void IsDamaged(int quantity) {
    IsHealed(-quantity);

    if (_health.IsDead())
      Death();
  }

  public void Show() {
    bar.Show(_health.Get(), _health.GetMax());
  }

  public void Death() {
  }

  [SerializeField] private Health _health;
  public HealthBar bar;
}
