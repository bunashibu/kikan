using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class FloatReactiveGauge {
    public FloatReactiveGauge() {
      _cur = new FloatReactiveProperty();
      _min = new FloatReactiveProperty();
      _max = new FloatReactiveProperty();
    }

    public void Add(float quantity) {
      if (_cur.Value + quantity >= _max.Value)
        _cur.Value = _max.Value;
      else
        _cur.Value += quantity;
    }

    public void Subtract(float quantity) {
      if (_cur.Value - quantity <= _min.Value)
        _cur.Value = _min.Value;
      else
        _cur.Value -= quantity;
    }

    public IReadOnlyReactiveProperty<float> Cur => _cur;
    public IReadOnlyReactiveProperty<float> Min => _min;
    public IReadOnlyReactiveProperty<float> Max => _max;

    public FloatReactiveProperty _cur { get; protected set; }
    public FloatReactiveProperty _min { get; protected set; }
    public FloatReactiveProperty _max { get; protected set; }
  }
}

