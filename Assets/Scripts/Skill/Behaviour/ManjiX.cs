using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void Update() {
  }

  void OnTriggerEnter2D(Collider2D collider) {
    var target = collider.gameObject;

    if (target.tag == "Enemy") {
      // damage = _atk * _power;
      target.GetComponent<HealthSystem>().IsDamaged(10);
      target.GetComponent<Enemy>().ShowHealthBar();
    }
  }

  public override void Behave() {
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private float _power;
}

