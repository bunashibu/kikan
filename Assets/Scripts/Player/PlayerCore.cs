using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  void Update() {
    if (photonView.isMine) {
      bool lvUpCriticalRequest = Input.GetKeyDown(_criticalCore.Key);

      if (lvUpCriticalRequest) {
        Debug.Log("lvUpCriticalRequest is true");

        if(_isReconfirming) {
          Debug.Log("LvUp");
          _criticalCore.LvUp();
          UpdateCriticalView(_criticalCore.Level);
          _isReconfirming = false;
        } else {
          Debug.Log("Reconfirming now");
          _isReconfirming = true;
        }
      }
    }
  }

  public void Init(CorePanel corePanel) {
    _corePanel = corePanel;
  }

  private void UpdateAttackView(int level) {
    _corePanel.Attack.UpdateView(level);
  }

  private void UpdateCriticalView(int level) {
    _corePanel.Critical.UpdateView(level);
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
  private bool _isReconfirming = false;
}

