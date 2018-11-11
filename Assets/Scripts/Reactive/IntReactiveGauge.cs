using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class IntReactiveGauge {
    public IntReactiveGauge() {
      _cur = new IntReactiveProperty();
      _min = new IntReactiveProperty();
      _max = new IntReactiveProperty();
    }

    public IntReactiveGauge(int initialValue) : this() {
      _cur.Value = initialValue;
      _min.Value = initialValue;
      _max.Value = initialValue;
    }

    public void Add(int quantity) {
      _cur.Value += quantity;
      AdjustBoundary();
    }

    public void Subtract(int quantity) {
      _cur.Value -= quantity;
      AdjustBoundary();
    }

    private void AdjustBoundary() {
      if (_cur.Value < _min.Value)
        _cur.Value = _min.Value;
      if (_cur.Value > _max.Value)
        _cur.Value = _max.Value;
    }

    public IReadOnlyReactiveProperty<int> Cur => _cur;
    public IReadOnlyReactiveProperty<int> Min => _min;
    public IReadOnlyReactiveProperty<int> Max => _max;

    public IntReactiveProperty _cur { get; private set; }
    public IntReactiveProperty _min { get; private set; }
    public IntReactiveProperty _max { get; private set; }
  }
}

