using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;
      var targetID = target.GetComponent<PhotonView>().viewID;

      if (targetID != _viewID) {
        if (target.tag == "Player") {
          var hp = target.GetComponent<PlayerHp>();
          hp.Minus(10);
          hp.Show();

          if (hp.Dead) {
            var skillUser = PhotonView.Find(_viewID).gameObject;

            var killExp = target.GetComponent<KillExp>().Exp;
            var nextExp = skillUser.GetComponent<PlayerNextExp>();
            nextExp.Plus(killExp);
            nextExp.Show();

            skillUser.GetComponent<PlayerKillDeathRecorder>().RecordKill();
            target.GetComponent<PlayerKillDeathRecorder>().RecordDeath();
          }
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
}

