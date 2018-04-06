using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HealArea : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
      var targetObj = collider.gameObject;
      if (targetObj.tag != "Player")
        return;

      var target = targetObj.GetComponent<BattlePlayer>();

      if (!target.PhotonView.isMine)
        return;

      if (target.PlayerInfo.Team == _team)
        target.Hp.FullRecover();
      else
        target.Hp.Subtract(1000);

      target.Hp.UpdateView();
    }

    [SerializeField] private int _team;
  }
}

