using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  void Update() {
    if (photonView.isMine) {
      bool unlockE = Input.GetKeyDown(KeyCode.E);

      if (unlockE) {

      }
    }
  }

  public void Init(CorePanel corePanel) {
    _corePanel = corePanel;
  }

  public void UpdateCriticalView(int level) {
    //_corePanel.UpdateCriticalView(level);
  }

  public int Attack {
    get {
      return _attackCore.Value;
    }
  }

  public int Critical {
    get {
      return _criticalCore.Value;
    }
  }

  [SerializeField] private Core _attackCore;
  [SerializeField] private Core _criticalCore;
  private CorePanel _corePanel;
}

