using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class AttackSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncAttackRPC(int attackerViewID, int targetViewID, int damage, bool isCritical, HitEffectType hitEffectType) {
      var attacker = PhotonView.Find(attackerViewID).gameObject.GetComponent<IAttacker>();
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();

      Assert.IsNotNull(attacker);
      Assert.IsNotNull(target);

      target.OnAttacked(attacker, damage, isCritical, hitEffectType);
    }

    public void SyncAttack(int attackerViewID, int targetViewID, int damage, bool isCritical, HitEffectType hitEffectType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncAttackRPC", PhotonTargets.AllViaServer, attackerViewID, targetViewID, damage, isCritical, hitEffectType);
    }
  }
}

