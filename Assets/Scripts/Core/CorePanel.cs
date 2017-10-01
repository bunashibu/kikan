using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CorePanel : MonoBehaviour {
    public List<CoreChart> ChartList {
      get {
        return _chartList;
      }
    }

    [SerializeField] private List<CoreChart> _chartList;
  }
}

