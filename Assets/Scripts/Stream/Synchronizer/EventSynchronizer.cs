using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EventSynchronizer : Photon.MonoBehaviour {
    [PunRPC]
    public void SyncPlayerInitializeRPC(int viewID) {
      var player = PhotonView.Find(viewID).gameObject.GetComponent<Player>();
      Assert.IsNotNull(player);

      EventStream.OnNextPlayerInitialize(player);
    }

    public void SyncPlayerInitialize(int viewID) {
      photonView.RPC("SyncPlayerInitializeRPC", PhotonTargets.All, viewID);
    }
  }
}

