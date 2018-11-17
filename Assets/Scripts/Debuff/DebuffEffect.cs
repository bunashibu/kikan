using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DebuffEffect {
    public DebuffEffect() {
      _effectPrefab = new Dictionary<DebuffType, GameObject>();
      _existEffect = new Dictionary<DebuffType, GameObject>();
    }

    public void Register(DebuffType key, GameObject effectPrefab) {
      _effectPrefab[key] = effectPrefab;
    }

    public void Instantiate(DebuffType key) {
      if (_effectPrefab.ContainsKey(key))
        _existEffect[key] = GameObject.Instantiate(_effectPrefab[key]);
    }

    public void Destroy(DebuffType key) {
      if (_existEffect.ContainsKey(key))
        GameObject.Destroy(_existEffect[key]);
    }

    private Dictionary<DebuffType, GameObject> _effectPrefab;
    private Dictionary<DebuffType, GameObject> _existEffect;
  }
}

