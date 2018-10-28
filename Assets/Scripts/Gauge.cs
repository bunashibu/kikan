using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Gauge : Duplexer {
    public Gauge() {
      Cur = 0;
      Min = 0;
      Max = 0;
    }

    protected void Add(int quantity) {
      Cur += quantity;
      AdjustBoundary();
    }

    protected void Subtract(int quantity) {
      Cur -= quantity;
      AdjustBoundary();
    }

    private void AdjustBoundary() {
      if (Cur < Min)
        Cur = Min;
      if (Cur > Max)
        Cur = Max;
    }

    public int Cur { get; protected set; }
    public int Min { get; protected set; }
    public int Max { get; protected set; }
  }
}

