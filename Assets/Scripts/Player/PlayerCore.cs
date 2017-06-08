using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : Photon.MonoBehaviour {
  void Awake() {
    _reconfirmList = new List<bool>() { false, false, false, false, false };
  }

  void Update() {
    if (photonView.isMine) {
      for (int i=0; i<_coreList.Count; ++i) {
        bool lvUpRequest = Input.GetKeyDown(_coreList[i].Key);

        // HACK: Too dirty
        if (lvUpRequest) {
          if (_reconfirmList[i]) {
            bool isAffordable = (_playerGold.Cur >= _coreList[i].Gold);

            if (isAffordable) {
              _playerGold.Minus(_coreList[i].Gold);
              _playerGold.UpdateView();

              _coreList[i].LvUp();
              UpdateView(i, _coreList[i].Level);
              _reconfirmList[i] = false;
            } else {
              Debug.Log("You don't have enough gold.");
            }
          } else {
            _reconfirmList[i] = true;
          }
        }
      }
    }
  }

  public void Init(CorePanel corePanel) {
    _corePanel = corePanel;
  }

  // HACK: Maybe it should use array
  private void UpdateView(int index, int level) {
    switch (index) {
      case 0:
        _corePanel.Speed.UpdateView(level);
        break;
      case 1:
        _corePanel.Hp.UpdateView(level);
        break;
      case 2:
        _corePanel.Attack.UpdateView(level);
        break;
      case 3:
        _corePanel.Critical.UpdateView(level);
        break;
      case 4:
        _corePanel.Heal.UpdateView(level);
        break;
      default:
        Debug.Log("Error: PlayerCore-UpdateView argument-index is wrong");
        break;
    }
  }

  public int Speed {
    get {
      return _coreList[0].Value;
    }
  }

  public int Hp {
    get {
      return _coreList[1].Value;
    }
  }

  public int Attack {
    get {
      return _coreList[2].Value;
    }
  }

  public int Critical {
    get {
      return _coreList[3].Value;
    }
  }

  public int Heal {
    get {
      return _coreList[4].Value;
    }
  }

  [SerializeField] private PlayerGold _playerGold;
  [SerializeField] private List<Core> _coreList;
  private CorePanel _corePanel;
  private List<bool> _reconfirmList;
}

