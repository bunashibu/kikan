using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  void Update() {
    if (photonView.isMine) {
      bool lvUpCriticalRequest = Input.GetKeyDown(_criticalCore.Key);

      if (lvUpCriticalRequest) {

        if(_isReconfirming) {

          _isAffordable = (_playerGold.Cur >= _criticalCore.Gold);

          if (_isAffordable) {
            _playerGold.Minus(_criticalCore.Gold);
            _playerGold.UpdateView();

            _criticalCore.LvUp();
            UpdateCriticalView(_criticalCore.Level);
            _isReconfirming = false;

          } else {
            Debug.Log("You don't have enough gold.");
          }

        } else {
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

  [SerializeField] private PlayerGold _playerGold;
  [SerializeField] private Core _attackCore;
  [SerializeField] private Core _criticalCore;
  private CorePanel _corePanel;
  private bool _isReconfirming = false;
  private bool _isAffordable = false;
}

