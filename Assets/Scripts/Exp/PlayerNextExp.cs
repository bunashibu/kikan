using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerNextExp : NextExp {
  public void Init(Bar hudBar) {
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncExpInit", PhotonTargets.All);
    _hudBar = hudBar;
  }

  [PunRPC]
  private void SyncExpShow() {
    if (photonView.isMine)
      _hudBar.Show(Cur, Max);
  }

  public void Show() {
    photonView.RPC("SyncExpShow", PhotonTargets.All);
  }

  private Bar _hudBar;
}

