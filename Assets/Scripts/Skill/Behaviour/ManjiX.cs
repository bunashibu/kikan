using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    var target = collider.gameObject;

    if (target.tag == "Enemy") {
      int damage = _status.atk + _power;
      target.GetComponent<HealthSystem>().IsDamaged(damage);
      target.GetComponent<Enemy>().ShowHealthBar();
    }

    if (target.tag == "Player") {
      int damage = _status.atk + _power;
      var hs = target.GetComponent<HealthSystem>();
      hs.IsDamaged(damage);
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
}

