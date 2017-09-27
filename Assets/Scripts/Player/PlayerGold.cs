using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerGold : Gold {
    public void Init(GoldPanel goldPanel) {
      Assert.IsTrue(photonView.isMine);
  
      photonView.RPC("SyncGoldInit", PhotonTargets.All);
      _goldPanel = goldPanel;
    }
  
    [PunRPC]
    private void SyncGoldUpdate(int cur) {
      if (photonView.isMine)
        _goldPanel.UpdateGold(cur);
    }
  
    public void UpdateView() {
      photonView.RPC("SyncGoldUpdate", PhotonTargets.All, Cur);
    }
  
    private GoldPanel _goldPanel;
  }
}

