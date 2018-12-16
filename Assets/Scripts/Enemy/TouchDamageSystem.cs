using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TouchDamageSystem : MonoBehaviour {
    void Awake() {
      _synchronizer = GetComponent<AttackSynchronizer>();
    }

    void OnTriggerStay2D(Collider2D collider) {
      var targetObj = collider.gameObject;
      if (targetObj.tag != "Player")
        return;

      var target = targetObj.GetComponent<Player>();
      if (!target.PhotonView.isMine)
        return;
      if (target.State.Invincible)
        return;

      var damage = _baseDamage + (int)(Random.value * _positiveDeviation);
      var attackerViewID = GetComponent<IPhoton>().PhotonView.viewID;

      _synchronizer.SyncAttack(attackerViewID, target.PhotonView.viewID, damage, false, HitEffectType.None);

      target.State.Invincible = true;
      MonoUtility.Instance.DelaySec(2.0f, () => { target.State.Invincible = false; } );
    }

    [SerializeField] private int _baseDamage;
    [SerializeField] private int _positiveDeviation;
    private AttackSynchronizer _synchronizer;
  }
}

