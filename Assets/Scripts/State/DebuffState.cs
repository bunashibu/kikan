using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DebuffState {
    public DebuffState() {
      _debuffDictionary = new Dictionary<DebuffType, bool>();
    }

    public DebuffState(params DebuffType[] types) : this() {
      Register(types);
    }

    public void Register(params DebuffType[] types) {
      foreach (var type in types)
        _debuffDictionary[type] = false;
    }

    public void DurationEnable(DebuffType debuffType, float duration) {
      Enable(debuffType);

      MonoUtility.Instance.DelaySec(duration, () => {
        Disable(debuffType);
      });
    }

    public void Enable(DebuffType debuffType) {
      if (_debuffDictionary.ContainsKey(debuffType))
        _debuffDictionary[debuffType] = true;
    }

    public void Disable(DebuffType debuffType) {
      if (_debuffDictionary.ContainsKey(debuffType))
        _debuffDictionary[debuffType] = false;
    }

    private Dictionary<DebuffType, bool> _debuffDictionary;
  }
}

