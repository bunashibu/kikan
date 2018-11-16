using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class DebuffSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncDebuffRPC(int targetViewID, DebuffType debuffType) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IBattle>();
      Assert.IsNotNull(target);

      target.OnDebuffed(debuffType);
    }

    public void SyncDebuff(int targetViewID, DebuffType debuffType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncDebuffRPC", PhotonTargets.All, targetViewID, debuffType);
    }
  }
}

