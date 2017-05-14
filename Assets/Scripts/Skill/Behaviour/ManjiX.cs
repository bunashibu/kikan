using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;
      var targetUser = target.GetComponent<PhotonView>().owner;

      if (targetUser != _skillUser) {
        if (target.tag == "Player") {
          var health = target.GetComponent<PlayerHealth>();
          health.Minus(10);
          health.Show();

          if (health.Dead)
            _kdRecoder.Kill(target);
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private KillDeathRecoder _kdRecoder;
  [SerializeField] private int _power;
}

