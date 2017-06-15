using UnityEngine;
using System.Collections;

public class ManjiShift : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      /*
      var target = collider.gameObject;
      var targetUser = target.GetComponent<PhotonView>().owner;

      if (targetUser != _skillUser) {
        if (target.tag == "Player") {
          var health = target.GetComponent<PlayerHealth>();
          health.Minus(50);
          health.Show();
        }
      }
      */
    }
  }

  [SerializeField] private BoxCollider2D _collider;
}

