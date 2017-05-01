using UnityEngine;
using System.Collections;

public class Health : Photon.MonoBehaviour, IGauge<int> {
  public void Init(int life, int maxLife) {
    Cur = life;
    Min = 0; // Until C#6
    Max = maxLife;
  }

  [PunRPC]
  public void SyncCur(int val) {
    Cur = val;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur < Min)
      Cur = Min;
    if (Cur > Max)
      Cur = Max;

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
  public int Min { get; private set; } // = 0; C#6
  public int Max { get; private set; }
  public bool Dead { get; private set; }
}

