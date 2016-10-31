using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  public void IsHealed(int quantity) {
    _health.Plus(quantity);
    Show();
  }

  public void IsDamaged(int quantity) {
    IsHealed(-quantity);
  }

  public void Show() {
    bar.Show(_health.Get(), _health.GetMax());
  }

  public void Set(int life, int maxLife) {
    _health.Set(life, maxLife);
  }

  [SerializeField] private Health _health;
  public HealthBar bar;
}
