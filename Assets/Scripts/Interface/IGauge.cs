using UnityEngine;
using System.Collections;

public interface IGauge<T> {
  T Cur { get; }
  T Min { get; }
  T Max { get; }
  void Plus(T quantity);
  void Minus(T quantity);
}

