using UnityEngine;
using System.Collections;

public class Health : Photon.MonoBehaviour, IGauge<int> {
  [PunRPC]
  protected void SyncInit(int cur, int min, int max) {
    Cur = cur;
    Min = min;
    Max = max;
  }

  [PunRPC]
  protected void SyncCur(int cur) {
    Cur = cur;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur < Min)
      Cur = Min;
    if (Cur > Max)
      Cur = Max;

    photonView.RPC("SyncCur", PhotonTargets.Others, Cur);

    if (Cur == Min)
      Die();
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  public virtual void Die() {
    Dead = true;
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  public bool Dead { get; private set; }
}

