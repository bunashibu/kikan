using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class DebuffSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncDebuffRPC(int targetViewID, DebuffType debuffType, float duration) {
      var target = PhotonView.Find(targetViewID).gameObject.GetComponent<IOnDebuffed>();
      Assert.IsNotNull(target);

      target.OnDebuffed(debuffType, duration);
    }

    public void SyncDebuff(int targetViewID, DebuffType debuffType, float duration) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      photonView.RPC("SyncDebuffRPC", PhotonTargets.All, targetViewID, debuffType, duration);
    }
  }
}

