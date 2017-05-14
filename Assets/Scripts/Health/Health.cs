using UnityEngine;
using System.Collections;

public class Health : Photon.MonoBehaviour, IGauge<int> {
  [PunRPC]
  protected void SyncHpInit(int cur, int min, int max) {
    Cur = cur;
    Min = min;
    Max = max;
  }

  [PunRPC]
  protected void SyncHpCur(int cur) {
    Cur = cur;
  }

  [PunRPC]
  protected void SyncHpDead(bool dead) {
    Dead = dead;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur <= Min)
      Die();
    if (Cur > Max)
      Cur = Max;

    photonView.RPC("SyncHpCur", PhotonTargets.Others, Cur);

    if (Dead) {
      if (Cur > Min)
        Reborn();

      photonView.RPC("SyncHpDead", PhotonTargets.Others, Dead);
    }
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  private void Die() {
    Dead = true;
    Cur = Min;
  }

  private void Reborn() {
    Dead = false;
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  public bool Dead { get; private set; }
}

