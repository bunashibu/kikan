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

      var flow = new AttackFlow(attacker, target, damage, isCritical, hitEffectType);
      SkillStream.OnNextAttack(flow);
    }

    public void SyncAttack(int attackerViewID, int targetViewID, int damage, bool isCritical, HitEffectType hitEffectType) {
      photonView.RPC("SyncAttackRPC", PhotonTargets.AllViaServer, attackerViewID, targetViewID, damage, isCritical, hitEffectType);
    }

    [PunRPC]
    private void SyncDebuffRPC(int targetViewID, DebuffType debuffType, float duration) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnDebuffed>();
      Assert.IsNotNull(target);

      var flow = new DebuffFlow(target, debuffType, duration);
      SkillStream.OnNextDebuff(flow);
    }

    public void SyncDebuff(int targetViewID, DebuffType debuffType, float duration) {
      photonView.RPC("SyncDebuffRPC", PhotonTargets.AllViaServer, targetViewID, debuffType, duration);
    }

    [PunRPC]
    private void SyncHealRPC(int targetViewID, int quantity) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();
      Assert.IsNotNull(target);

      var flow = new HealFlow(target, quantity);
      SkillStream.OnNextHeal(flow);
    }

    public void SyncHeal(int targetViewID, int quantity) {
      photonView.RPC("SyncHealRPC", PhotonTargets.AllViaServer, targetViewID, quantity);
    }

    [PunRPC]
    private void SyncForceRPC(int targetViewID, float force, Vector2 direction, bool isNewAdd) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnForced>();
      Assert.IsNotNull(target);

      var flow = new ForceFlow(target, force, direction, isNewAdd);
      SkillStream.OnNextForce(flow);
    }

    public void SyncForce(int targetViewID, float force, Vector2 direction, bool isNewAdd) {
      photonView.RPC("SyncForceRPC", PhotonTargets.AllViaServer, targetViewID, force, direction, isNewAdd);
    }

    [PunRPC]
    private void SyncStatusRPC(int targetViewID, int fixAtk) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IStatus>();
      Assert.IsNotNull(target);

      var flow = new StatusFlow(target, fixAtk);
      SkillStream.OnNextStatus(flow);
    }

    public void SyncStatus(int targetViewID, int fixAtk) {
      photonView.RPC("SyncStatusRPC", PhotonTargets.AllViaServer, targetViewID, fixAtk);
    }
  }
}
