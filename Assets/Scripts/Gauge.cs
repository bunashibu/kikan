using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Gauge<T> {
    public T Cur { get; protected set; }
    public T Min { get; protected set; }
    public T Max { get; protected set; }

    public abstract void Add(T quantity);
    public abstract void Subtract(T quantity);
  }
}

