using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class ReactiveState<T> {
    public ReactiveState() {
      _state = new ReactiveDictionary<T, bool>();
    }

    public ReactiveState(params T[] keys) : this() {
      Register(keys);
    }

    public void Register(params T[] keys) {
      foreach (var key in keys)
        _state[key] = false;
    }

    public void Enable(T key) {
      if (_state.ContainsKey(key))
        _state[key] = true;
    }

    public void Disable(T key) {
      if (_state.ContainsKey(key))
        _state[key] = false;
    }

    public IReadOnlyReactiveDictionary<T, bool> State => _state;

    private ReactiveDictionary<T, bool> _state;
  }
}

