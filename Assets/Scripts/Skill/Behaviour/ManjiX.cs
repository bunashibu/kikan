using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;
      var targetView = target.GetComponent<PhotonView>();

      if (targetView.owner != _skillUser) {
        if (target.tag == "Enemy") {
          int damage = _power;
          target.GetComponent<EnemyHealth>().Minus(damage);
          target.GetComponent<Enemy>().ShowHealthBar();
        }

        if (target.tag == "Player") {
          var health = target.GetComponent<PlayerHealth>();
          health.Minus(10);
          targetView.RPC("Show", PhotonTargets.All);
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
}

