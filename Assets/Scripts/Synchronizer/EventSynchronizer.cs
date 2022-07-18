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

    // TEMP: deal with simultaneous heal/attack bug temporarily
    [PunRPC]
    public void SyncPlayerHpRPC(int viewID, int hp) {
      var player = PhotonView.Find(viewID).gameObject.GetComponent<Player>();
      Assert.IsNotNull(player);

      player.Hp.Set(hp);
    }

    // TEMP: deal with simultaneous heal/attack bug temporarily
    public void SyncPlayerHp(int viewID, int hp) {
      photonView.RPC("SyncPlayerHpRPC", PhotonTargets.All, viewID, hp);
    }
  }
}
