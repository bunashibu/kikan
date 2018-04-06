using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TeamArea : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
      var targetObj = collider.gameObject;
      if (targetObj.tag != "Player")
        return;

      var target = targetObj.GetComponent<BattlePlayer>();

      if (!target.PhotonView.isMine)
        return;

      if (target.PlayerInfo.Team == _team) {
        if (target.Hp.Cur == target.Hp.Max)
          return;

        target.Hp.FullRecover();
      }
      else {
        if (target.State.Invincible)
          return;

        target.State.Invincible = true;
        MonoUtility.Instance.DelaySec(2.0f, () => { target.State.Invincible = false; } );
        target.Hp.Subtract(_damage);
        target.NumberPopupEnvironment.Popup(_damage, false, target.DamageSkin.Id, PopupType.Player);
      }

      target.Hp.UpdateView();
    }

    [SerializeField] private int _team;
    [SerializeField] private int _damage;
  }
}

