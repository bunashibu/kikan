using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public abstract class Gauge<T> : IObserver {
    public abstract void OnNotify(Notification notification, object[] args);

    public abstract void Add(T quantity);
    public abstract void Subtract(T quantity);

    public T Cur { get; protected set; }
    public T Min { get; protected set; }
    public T Max { get; protected set; }
  }
}

