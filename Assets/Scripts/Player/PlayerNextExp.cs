using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerNextExp : NextExp {
    public void Init(Bar hudBar) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncExpInit", PhotonTargets.All);
      _hudBar = hudBar;
    }

    [PunRPC]
    private void SyncExpUpdate() {
      if (photonView.isMine)
        _hudBar.UpdateView(Cur, Max);
    }

    public void UpdateView() {
      photonView.RPC("SyncExpUpdate", PhotonTargets.All);
    }

    private Bar _hudBar;
  }
}

