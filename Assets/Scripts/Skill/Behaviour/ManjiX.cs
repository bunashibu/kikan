using UnityEngine;
using System.Collections;

public class ManjiX : MonoBehaviour {
  public void Init(PlayerStatus status) {
    _status = status;
  }

  void OnTriggerEnter2D(Collider2D collider) {
    var target = collider.gameObject;

    if (target.tag == "Enemy") {
      int damage = _status.atk * _power;
      target.GetComponent<HealthSystem>().IsDamaged(damage);
      target.GetComponent<Enemy>().ShowHealthBar();
    }

    if (target.tag == "Player") {

    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  private PlayerStatus _status;
}

