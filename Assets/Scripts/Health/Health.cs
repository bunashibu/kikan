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

  [PunRPC]
  protected void SyncDead(bool dead) {
    Dead = dead;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur < Min)
      Cur = Min;
    if (Cur > Max)
      Cur = Max;

    photonView.RPC("SyncCur", PhotonTargets.Others, Cur);

    if (Cur == Min) {
      Die();
      photonView.RPC("SyncDead", PhotonTargets.Others, Dead);
    }
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  private void Die() {
    Dead = true;
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  public bool Dead { get; private set; }
}

