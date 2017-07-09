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

  public void Add(int quantity) {
    Cur += quantity;

    if (Cur > Max)
      Cur = Max;
    if (Cur < Min)
      Cur = Min;

    photonView.RPC("SyncGoldCur", PhotonTargets.Others, Cur);
  }

  public void Subtract(int quantity) {
    Add(-quantity);
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
}

