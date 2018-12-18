using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  public class PlayerStreamSynchronizer : Photon.MonoBehaviour {
    public void SetStream(PlayerStream stream) {
      _stream = stream;
    }

    [PunRPC]
    private void SyncCoreLevelUpRPC(CoreType type) {
      _stream.OnNextCore(type);
    }

    public void SyncCoreLevelUp(CoreType type) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncCoreLevelUpRPC", PhotonTargets.AllViaServer, type);
    }

    [PunRPC]
    private void SyncAutoHealRPC(int quantity) {
      _stream.OnNextAutoHeal(quantity);
    }

    public void SyncAutoHeal(int quantity) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncAutoHealRPC", PhotonTargets.AllViaServer, quantity);
    }

    [PunRPC]
    private void SyncRespawnRPC(int viewID) {
      _stream.OnNextRespawn(viewID);
    }

    public void SyncRespawn(int viewID) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncRespawnRPC", PhotonTargets.AllViaServer, viewID);
    }

    private PlayerStream _stream;
  }
}

