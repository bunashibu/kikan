using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : IGauge<int> {
  public void Plus(int quantity) {
    Cur += quantity;
    AdjustBoundary();
  }

  public void Minus(int quantity) {
    Cur -= quantity;
    AdjustBoundary();
  }

  private void AdjustBoundary() {
    if (Cur <= Min)
      Die();
    if (Cur > Max)
      Cur = Max;
  }

  protected virtual void Die() {
    IsDead = true;
    Cur = Min;
  }

  public int Cur { get; protected set; }
  public int Min { get; protected set; }
  public int Max { get; protected set; }
  public bool IsDead { get; protected set; }
}

