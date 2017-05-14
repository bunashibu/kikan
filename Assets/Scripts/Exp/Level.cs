using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Photon.MonoBehaviour {
  void Awake() {
    Cur = 1;
  }

  [PunRPC]
  private void SyncCur(int cur) {
    Cur = cur;
  }

  public void LvUp() {
    Cur += 1;
    photonView.RPC("SyncCur", PhotonTargets.Others, Cur);
  }

  public int Cur { get; private set; }
}

