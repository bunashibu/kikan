using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class ReactiveState<T> {
    public ReactiveState() {
      _stateDictionary = new ReactiveDictionary<T, bool>();
    }

    public ReactiveState(params T[] states) : this() {
      Register(states);
    }

    public void Register(params T[] states) {
      foreach (var state in states)
        _stateDictionary[state] = false;
    }

    public void DurationEnable(T state, float duration) {
      Enable(state);

      MonoUtility.Instance.DelaySec(duration, () => {
        Disable(state);
      });
    }

    public void Enable(T state) {
      if (_stateDictionary.ContainsKey(state))
        _stateDictionary[state] = true;
    }

    public void Disable(T state) {
      if (_stateDictionary.ContainsKey(state))
        _stateDictionary[state] = false;
    }

    public IReadOnlyReactiveDictionary<T, bool> StateDictionary => _stateDictionary;

    private ReactiveDictionary<T, bool> _stateDictionary;
  }
}

