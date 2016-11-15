using UnityEngine;
using System.Collections;

public class HealthSystem : MonoBehaviour {
  void Awake() {
    _health = ScriptableObject.CreateInstance("Health") as Health;
  }

  public void Init(int life, int maxLife, BattleSceneManager manager) {
    _health.Init(life, maxLife);

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

  void Die() {
    _anim.SetBool("Die", true);
  }

  [SerializeField] private Health _health;
  [SerializeField] private Animator _anim;
  [SerializeField] private HealthBar _bar;
}
