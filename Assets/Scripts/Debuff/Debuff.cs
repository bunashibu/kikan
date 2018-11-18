using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class Debuff {
    public Debuff(Transform parent) {
      _state = new ReactiveState<DebuffType>();
      _effect = new DebuffEffect(parent);
    }

    public void Register(DebuffType type, GameObject effect) {
      _state.Register(type);
      _effect.Register(type, effect);
    }

    public void DurationEnable(DebuffType type, float duration) {
      _state.Enable(type);

      MonoUtility.Instance.DelaySec(duration, () => {
        _state.Disable(type);
      });
    }

    public void Instantiate(DebuffType type) {
      _effect.Instantiate(type);
    }

    public void Destroy(DebuffType type) {
      _effect.Destroy(type);
    }

    public IReadOnlyReactiveDictionary<DebuffType, bool> State => _state.State;

    private ReactiveState<DebuffType> _state;
    private DebuffEffect _effect;
  }
}

