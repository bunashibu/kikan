using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;
      var targetID = target.GetComponent<PhotonView>().viewID;

      if (targetID != _viewID) {
        if (target.tag == "Player") {
          var health = target.GetComponent<PlayerHealth>();
          health.Minus(10);
          health.Show();

          if (health.Dead) {
            var skillUser = PhotonView.Find(_viewID).gameObject;

            var killExp = target.GetComponent<KillExp>().Exp;
            skillUser.GetComponent<NextExp>().Plus(killExp);

            skillUser.GetComponent<KillDeathRecoder>().RecordKill();
            target.GetComponent<KillDeathRecoder>().RecordDeath();
          }
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
}

