using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  public void Init(CorePanel corePanel) {
    _corePanel = corePanel;
  }

  public void UpdateSpeedView() {
    _corePanel.UpdateSpeedView();
  }

  public void UpdateHpView() {
    _corePanel.UpdateHpView();
  }

  public void UpdateAttackView() {
    _corePanel.UpdateAttackView();
  }

  public void UpdateCriticalView() {
    _corePanel.UpdateCriticalView();
  }

  public void UpdateHealView() {
    _corePanel.UpdateHealView();
  }

  private CorePanel _corePanel;
  private CriticalCore _criticalCore;
}

