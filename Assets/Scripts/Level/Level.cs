using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Level : Photon.MonoBehaviour {
    public void Init(int initialLv) {
      Lv = initialLv;
      photonView.RPC("SyncLvCur", PhotonTargets.Others, Lv);
    }
  
    [PunRPC]
    protected void SyncLvCur(int lv) {
      Lv = lv;
    }
  
    public virtual void LvUp() {
      Lv += 1;
      photonView.RPC("SyncLvCur", PhotonTargets.Others, Lv);
    }
  
    public int Lv { get; private set; }
  }
}

