using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePanel : MonoBehaviour {
  public CoreChart Critical {
    get {
      return _criticalChart;
    }
  }

  [SerializeField] private CoreChart _criticalChart;
}

