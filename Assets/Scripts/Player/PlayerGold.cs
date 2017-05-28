using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerGold : Gold {
  public void Init(GoldPanel goldPanel) {
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncGoldInit", PhotonTargets.All);
    _goldPanel = goldPanel;
  }

  [PunRPC]
  private void SyncGoldShow(int cur) {
    if (photonView.isMine)
      _goldPanel.UpdateGold(cur);
  }

  public void Show() {
    photonView.RPC("SyncGoldShow", PhotonTargets.All, Cur);
  }

  private GoldPanel _goldPanel;
}

