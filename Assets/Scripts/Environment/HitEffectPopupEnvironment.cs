using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class HitEffectPopupEnvironment : SingletonMonoBehaviour<HitEffectPopupEnvironment> {
    protected override void Awake() {
      _index = new Dictionary<HitEffectType, int>();

      int i = -1;
      foreach (HitEffectType type in Enum.GetValues(typeof(HitEffectType))) {
        _index[type] = i;
        ++i;
      }
    }

    public void PopupHitEffect(HitEffectType type, IPhotonBehaviour target) {
      if (type == HitEffectType.None)
        return;

      Instantiate(_hitEffectPref[_index[type]], target.transform);
    }

    [SerializeField] private List<GameObject> _hitEffectPref;
    private Dictionary<HitEffectType, int> _index;
  }
}
