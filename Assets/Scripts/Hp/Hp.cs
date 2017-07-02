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
      Cur = Min;
    if (Cur > Max)
      Cur = Max;
  }

  public int Cur { get; protected set; }
  public int Min { get; protected set; }
  public int Max { get; protected set; }
}

