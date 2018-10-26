using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TouchDamageSystem : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
      var targetObj = collider.gameObject;
      if (targetObj.tag != "Player")
        return;

      var target = targetObj.GetComponent<BattlePlayer>();

      if (!target.PhotonView.isMine)
        return;

      if (target.State.Invincible)
        return;

      var damage = _baseDamage + (int)(Random.value * _positiveDeviation);
      target.Hp.Subtract(damage);
      target.Hp.UpdateView();
      //target.NumberPopupEnvironment.Popup(damage, false, target.DamageSkin.Id, PopupType.Player);

      target.State.Invincible = true;
      MonoUtility.Instance.DelaySec(2.0f, () => { target.State.Invincible = false; } );
    }

    [SerializeField] private int _baseDamage;
    [SerializeField] private int _positiveDeviation;
  }
}

