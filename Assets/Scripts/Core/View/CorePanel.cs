using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CorePanel : MonoBehaviour {
    void Awake() {
      _index = new Dictionary<CoreType, int>();
      _index[CoreType.Speed]    = 0;
      _index[CoreType.Hp]       = 1;
      _index[CoreType.Attack]   = 2;
      _index[CoreType.Critical] = 3;
      _index[CoreType.Heal]     = 4;
    }

    public void UpdateView(CoreType type, int level) {
      _chart[_index[type]].UpdateView(level);
    }

    [SerializeField] private List<CoreChart> _chart;
    private Dictionary<CoreType, int> _index;
  }
}

