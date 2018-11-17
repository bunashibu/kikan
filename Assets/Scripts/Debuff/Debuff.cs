using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class Debuff {
    public Debuff() {
      _state = new ReactiveState<DebuffType>();
      _effect = new DebuffEffect();
    }

    public Debuff(params DebuffType[] keys) : this() {
      _state.Register(keys);
    }

    public void DurationEnable(DebuffType key, float duration) {
      _state.Enable(key);

      MonoUtility.Instance.DelaySec(duration, () => {
        _state.Disable(key);
      });
    }

    public void Instantiate(DebuffType key) {
      _effect.Instantiate(key);
    }

    public void Destroy(DebuffType key) {
      _effect.Destroy(key);
    }

    public IReadOnlyReactiveDictionary<DebuffType, bool> State => _state.State;

    private ReactiveState<DebuffType> _state;
    private DebuffEffect _effect;
  }
}

