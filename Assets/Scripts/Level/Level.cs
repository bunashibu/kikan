using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Photon.MonoBehaviour {
  public void Init() {
    Lv = 1;
  }

  [PunRPC]
  private void SyncCur(int lv) {
    Lv = lv;
  }

  public virtual void LvUp() {
    Lv += 1;
    photonView.RPC("SyncCur", PhotonTargets.Others, Lv);
  }

  public int Lv { get; private set; }
}

