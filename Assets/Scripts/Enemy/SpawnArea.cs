using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnArea : MonoBehaviour {
  public float CalculateY(float x, float offsetY = 0) {
    for (int i=0; i<_xRange.Count-1; ++i) {
      if (_xRange[i] <= x && x <= _xRange[i+1])
        return _a[i] * x + _b[i] + offsetY;
    }

    Assert.IsTrue(false); // Never come to here.
    return 0;
  }

  public float Adjust(float x) {
    if (x < _xRange.First())
      return x + (_xRange.First() - x) * 2;

    if (_xRange.Last() < x)
      return x - (x - _xRange.Last()) * 2;

    return x;
  }

  [Header("Slope")]
  [SerializeField] private List<float> _a;
  [Header("Segment")]
  [SerializeField] private List<float> _b;

  [Space(10)]
  [SerializeField] private List<float> _xRange;
}

