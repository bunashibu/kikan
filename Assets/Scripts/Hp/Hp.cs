using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Hp : IGauge<int> {
    public virtual void Add(int quantity) {
      Cur += quantity;
      AdjustBoundary();
    }

    public virtual void Subtract(int quantity) {
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
    public int Min { get { return 0; }   }
    public int Max { get; protected set; }
  }
}

