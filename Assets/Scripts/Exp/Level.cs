using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Photon.MonoBehaviour {
  void Awake() {
    Lv = 1;
  }

  [PunRPC]
  private void SyncCur(int lv) {
    Lv = lv;
  }

  public void LvUp() {
    Lv += 1;
    photonView.RPC("SyncCur", PhotonTargets.Others, Lv);
  }

  public int Lv { get; private set; }
}

