using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : IGauge<int> {
  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur <= Min)
      Die();
    if (Cur > Max)
      Cur = Max;

    if (IsDead && (Cur > Min))
      Reborn();
  }

  public void Minus(int quantity) {
    Plus(-quantity);
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

