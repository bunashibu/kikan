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

    public void Add(int quantity) {
      if (_cur.Value + quantity >= _max.Value)
        _cur.Value = _max.Value;
      else
        _cur.Value += quantity;
    }

    public void Subtract(int quantity) {
      if (_cur.Value - quantity <= _min.Value)
        _cur.Value = _min.Value;
      else
        _cur.Value -= quantity;
    }

    public IReadOnlyReactiveProperty<int> Cur => _cur;
    public IReadOnlyReactiveProperty<int> Min => _min;
    public IReadOnlyReactiveProperty<int> Max => _max;

    protected IntReactiveProperty _cur;
    protected IntReactiveProperty _min;
    protected IntReactiveProperty _max;
  }
}

