using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [System.Serializable]
  public class HitInfo {
    public int MaxTargetCount => _maxTargetCount;
    public int MaxHitCount    => _maxHitCount;
    public float Interval     => _interval;

    [SerializeField] private int _maxTargetCount;
    [SerializeField] private int _maxHitCount;
    [SerializeField] private float _interval;
  }
}

