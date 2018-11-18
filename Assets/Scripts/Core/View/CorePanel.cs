using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CorePanel : MonoBehaviour {
    public void UpdateView(int index, int level) {
      _chartList[index].UpdateView(level);
    }

    [SerializeField] private List<CoreChart> _chartList;
  }
}

