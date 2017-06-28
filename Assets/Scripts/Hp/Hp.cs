using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : IGauge<int> {
  public virtual void Plus(int quantity) {
    Cur += quantity;
    AdjustBoundary();
  }

  public virtual void Minus(int quantity) {
    Cur -= quantity;
    AdjustBoundary();
  }

  private void AdjustBoundary() {
    if (Cur <= Min)
      Die();
    if (Cur > Max)
      Cur = Max;

    if (IsDead && (Cur > Min))
      Reborn();
  }

  private void Die() {
    IsDead = true;
    Cur = Min;
  }

  private void Reborn() {
    IsDead = false;
  }

  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  public bool IsDead { get; private set; }
}

