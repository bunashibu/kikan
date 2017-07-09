using UnityEngine;
using System.Collections;

public interface IGauge<T> {
  T Cur { get; }
  T Min { get; }
  T Max { get; }
  void Add(T quantity);
  void Subtract(T quantity);
}

