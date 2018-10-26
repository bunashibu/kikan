using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class AttackSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncAttackRPC(int attackerViewID, int targetViewID, int damage, bool isCritical) {
      var attacker = PhotonView.Find(attackerViewID).gameObject;
      Assert.IsNotNull(attacker);

      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IMediator>();
      Assert.IsNotNull(target);

      var notifier = new Notifier(target.Mediator.OnNotify);
      notifier.Notify(Notification.TakeDamage, attacker, damage, isCritical);
    }

    public void SyncAttack(int attackerViewID, int targetViewID, int damage, bool isCritical) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncAttackRPC", PhotonTargets.All, attackerViewID, targetViewID, damage, isCritical);
    }
  }
}

