using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class HealSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncHealRPC(int targetViewID, int quantity) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnAttacked>();
      Assert.IsNotNull(target);

      target.Hp.Add(quantity);
    }

    public void SyncHeal(int targetViewID, int quantity) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncHealRPC", PhotonTargets.AllViaServer, targetViewID, quantity);
    }
  }
}

