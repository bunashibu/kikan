using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePanel : MonoBehaviour {
  public CoreChart Speed {
    get {
      return _speedChart;
    }
  }

  public CoreChart Hp {
    get {
      return _hpChart;
    }
  }

  public CoreChart Attack {
    get {
      return _attackChart;
    }
  }

  public CoreChart Critical {
    get {
      return _criticalChart;
    }
  }

  public CoreChart Heal {
    get {
      return _healChart;
    }
  }

  [SerializeField] private CoreChart _speedChart;
  [SerializeField] private CoreChart _hpChart;
  [SerializeField] private CoreChart _attackChart;
  [SerializeField] private CoreChart _criticalChart;
  [SerializeField] private CoreChart _healChart;
}

