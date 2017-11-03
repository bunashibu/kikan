using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class AllSkinData : ScriptableObject {
    public DamageSkin GetSkin(int skinId) {
      return _skinList[skinId];
    }

    [SerializeField] private List<DamageSkin> _skinList;
  }
}

