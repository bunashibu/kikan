using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DebuffEffect {
    public DebuffEffect(Transform parent) {
      _parent = parent;
      _effectPrefab = new Dictionary<DebuffType, GameObject>();
      _existEffect = new Dictionary<DebuffType, GameObject>();
    }

    public void Register(DebuffType key, GameObject effectPrefab) {
      _effectPrefab[key] = effectPrefab;
    }

    public void Instantiate(DebuffType key) {
      if (_effectPrefab.ContainsKey(key))
        _existEffect[key] = GameObject.Instantiate(_effectPrefab[key], _parent);
    }

    public void Destroy(DebuffType key) {
      if (_existEffect.ContainsKey(key)) {
        GameObject.Destroy(_existEffect[key]);
        _existEffect.Remove(key);
      }
    }

    private Transform _parent;
    private Dictionary<DebuffType, GameObject> _effectPrefab;
    private Dictionary<DebuffType, GameObject> _existEffect;
  }
}

