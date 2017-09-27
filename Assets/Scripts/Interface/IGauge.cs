using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public interface IGauge<T> {
    T Cur { get; }
    T Min { get; }
    T Max { get; }
    void Add(T quantity);
    void Subtract(T quantity);
  }
}

