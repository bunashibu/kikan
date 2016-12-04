using UnityEngine;
using System.Collections;

public class ManjiXBehaviour : SkillBehaviour {
  void Update() {
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Enemy") {
      // damage to enemy
      // damage = _atk * _power;
    }
  }

  public override void Behave() {
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private float _power;
}

