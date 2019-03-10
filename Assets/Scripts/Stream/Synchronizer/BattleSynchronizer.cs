using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class BattleSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    public void SyncKillRPC(int attackerViewID) {
      var attacker = PhotonView.Find(attackerViewID).gameObject.GetComponent<IAttacker>();
      Assert.IsNotNull(attacker);

      BattleStream.OnNextKill(attacker);
    }

    public void SyncKill(IAttacker attacker) {
      Assert.IsTrue(attacker is IPhotonBehaviour);
      int attackerViewID = ((IPhotonBehaviour)attacker).PhotonView.viewID;

      photonView.RPC("SyncKillRPC", PhotonTargets.All, attackerViewID);
    }

    [PunRPC]
    public void SyncDieRPC(int targetViewID) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();
      Assert.IsNotNull(target);

      BattleStream.OnNextDie(target);
    }

    public void SyncDie(IOnAttacked target) {
      Assert.IsTrue(target is IPhotonBehaviour);
      int targetViewID = ((IPhotonBehaviour)target).PhotonView.viewID;

      photonView.RPC("SyncDieRPC", PhotonTargets.All, targetViewID);
    }
  }
}

