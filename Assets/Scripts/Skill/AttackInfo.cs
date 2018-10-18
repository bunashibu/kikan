using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class AttackInfo {
    public int DamagePercent   { get { return _damagePercent;   } }
    public int MaxDeviation    { get { return _maxDeviation;    } }
    public int CriticalPercent { get { return _criticalPercent; } }

    [SerializeField] private int _damagePercent;
    [SerializeField] private int _maxDeviation;
    [SerializeField] private int _criticalPercent;
  }
}

