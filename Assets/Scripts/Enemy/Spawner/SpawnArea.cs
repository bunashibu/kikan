using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SpawnArea : MonoBehaviour {
    void Awake() {
      _boxCollider = GetComponent<BoxCollider2D>();

      if (_xRange.Count == 0) {
        var posX = transform.position.x;
        var margin = 1;
        var halfX = _boxCollider.size.x / 2 - margin;
        _xRange.Add(posX - halfX);
        _xRange.Add(posX + halfX);
      }
    }

    public float CalculateY(float x, float offsetY = 0) {
      for (int i=0; i<_xRange.Count-1; ++i) {
        if (_xRange[i] <= x && x <= _xRange[i+1])
          return _a[i] * x + _b[i] + offsetY;
      }

      Debug.LogError("CalculateY is out of range");
      Debug.LogError(x);
      Debug.LogError(offsetY);

      return _a[0] * _xRange[0] + _b[0] + offsetY;
    }

    public bool IsInRange(float x) {
      return (_xRange.First() <= x && x <= _xRange.Last());
    }

    public float GetRandomX() {
      return UnityEngine.Random.Range(_xRange.First(), _xRange.Last());
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

    private BoxCollider2D _boxCollider;
  }
}
