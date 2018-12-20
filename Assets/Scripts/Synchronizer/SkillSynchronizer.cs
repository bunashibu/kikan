using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class SkillSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncAttackRPC(int attackerViewID, int targetViewID, int damage, bool isCritical, HitEffectType hitEffectType) {
      var attacker = PhotonView.Find(attackerViewID).gameObject.GetComponent<IAttacker>();
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();

      Assert.IsNotNull(attacker);
      Assert.IsNotNull(target);

      var flowEntity = new AttackFlowEntity(attacker, target, damage, isCritical, hitEffectType);
      SkillStream.OnNextAttack(flowEntity);
    }

    public void SyncAttack(int attackerViewID, int targetViewID, int damage, bool isCritical, HitEffectType hitEffectType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncAttackRPC", PhotonTargets.AllViaServer, attackerViewID, targetViewID, damage, isCritical, hitEffectType);
    }

    [PunRPC]
    private void SyncDebuffRPC(int targetViewID, DebuffType debuffType, float duration) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnDebuffed>();
      Assert.IsNotNull(target);

      var flowEntity = new DebuffFlowEntity(target, debuffType, duration);
      SkillStream.OnNextDebuff(flowEntity);
    }

    public void SyncDebuff(int targetViewID, DebuffType debuffType, float duration) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncDebuffRPC", PhotonTargets.AllViaServer, targetViewID, debuffType, duration);
    }

    [PunRPC]
    private void SyncHealRPC(int targetViewID, int quantity) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();
      Assert.IsNotNull(target);

      var flowEntity = new HealFlowEntity(target, quantity);
      SkillStream.OnNextHeal(flowEntity);
    }

    public void SyncHeal(int targetViewID, int quantity) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncHealRPC", PhotonTargets.AllViaServer, targetViewID, quantity);
    }
  }
}

