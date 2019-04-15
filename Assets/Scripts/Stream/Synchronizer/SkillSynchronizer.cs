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
      photonView.RPC("SyncHealRPC", PhotonTargets.AllViaServer, targetViewID, quantity);
    }

    [PunRPC]
    private void SyncForceRPC(int targetViewID, float force, Vector2 direction, bool isNewAdd) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnForced>();
      Assert.IsNotNull(target);

      var flowEntity = new ForceFlowEntity(target, force, direction, isNewAdd);
      SkillStream.OnNextForce(flowEntity);
    }

    public void SyncForce(int targetViewID, float force, Vector2 direction, bool isNewAdd) {
      photonView.RPC("SyncForceRPC", PhotonTargets.AllViaServer, targetViewID, force, direction, isNewAdd);
    }

    [PunRPC]
    private void SyncStatusRPC(int targetViewID, int fixAtk) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IStatus>();
      Assert.IsNotNull(target);

      var flowEntity = new StatusFlowEntity(target, fixAtk);
      SkillStream.OnNextStatus(flowEntity);
    }

    public void SyncStatus(int targetViewID, int fixAtk) {
      photonView.RPC("SyncStatusRPC", PhotonTargets.AllViaServer, targetViewID, fixAtk);
    }
  }
}

