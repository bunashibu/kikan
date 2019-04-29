using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [System.Serializable]
  public class AttackInfo {
    public AttackInfo(int damagePercent, int maxDeviation, int criticalPercent) {
      _damagePercent = damagePercent;
      _maxDeviation = maxDeviation;
      _criticalPercent = criticalPercent;
    }

    public int DamagePercent   => _damagePercent;
    public int MaxDeviation    => _maxDeviation;
    public int CriticalPercent => _criticalPercent;

    [SerializeField] private int _damagePercent;
    [SerializeField] private int _maxDeviation;
    [SerializeField] private int _criticalPercent;
  }
}

