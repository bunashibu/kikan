using UnityEngine;
using System.Collections;

public interface ISlider<T> {
  T Cur { get; }
  T Max { get; }
  void Plus(T quantity);
  void Minus(T quantity);
}

