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
    private void SyncForceRPC(int targetViewID, float force, Vector2 direction) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnForced>();
      Assert.IsNotNull(target);

      var flowEntity = new ForceFlowEntity(target, force, direction);
      SkillStream.OnNextForce(flowEntity);
    }

    public void SyncForce(int targetViewID, float force, Vector2 direction) {
      photonView.RPC("SyncForceRPC", PhotonTargets.AllViaServer, targetViewID, force, direction);
    }
  }
}

