using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DebuffEffect {
    public DebuffEffect(Transform parent) {
      _parent = parent;
      _prefab = new Dictionary<DebuffType, GameObject>();
      _existEffect = new Dictionary<DebuffType, GameObject>();
    }

    public void Register(DebuffType type, GameObject effect) {
      _prefab[type] = effect;
    }

    public void Instantiate(DebuffType type) {
      if (_prefab.ContainsKey(type))
        _existEffect[type] = GameObject.Instantiate(_prefab[type], _parent);
    }

    public void Destroy(DebuffType type) {
      if (_existEffect.ContainsKey(type)) {
        GameObject.Destroy(_existEffect[type]);
        _existEffect.Remove(type);
      }
    }

    private Transform _parent;
    private Dictionary<DebuffType, GameObject> _prefab;
    private Dictionary<DebuffType, GameObject> _existEffect;
  }
}

