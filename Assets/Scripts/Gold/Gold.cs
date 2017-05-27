using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Photon.MonoBehaviour, IGauge<int> {
  [PunRPC]
  protected void SyncGoldInit() {
    Cur = 0;
    Min = 0;
    Max = 99999;
  }

  [PunRPC]
  protected void SyncGoldCur(int cur) {
    Cur = cur;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur > Max)
      Cur = Max;
    if (Cur < Min)
      Cur = Min;

    photonView.RPC("SyncGoldCur", PhotonTargets.Others, Cur);
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
}

