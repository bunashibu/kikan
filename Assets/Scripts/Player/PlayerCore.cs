using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerCore : Photon.MonoBehaviour {
    void Awake() {
      _reconfirmList = new List<bool>() { false, false, false, false, false };
      _coreList = _coreList.Select(core => ScriptableObject.Instantiate(core) as Core).ToList();
    }

    void Update() {
      if (!photonView.isMine)
        return;

      for (int i=0; i<_coreList.Count; ++i) {
        bool lvUpRequest = Input.GetKeyDown(_coreList[i].Key);
        if (lvUpRequest == false)
          continue;

        if (_reconfirmList[i] == false) {
          _reconfirmList[i] = true;
          return;
        }

        bool isMaxLevel = (_coreList[i].Level == MaxCoreLevel);
        if (isMaxLevel) {
          Debug.Log("Your core level is already max");
          return;
        }

        bool notEnoughGold = (_player.Gold.Cur < _coreList[i].Gold);
        if (notEnoughGold) {
          Debug.Log("You don't have enough gold");
          return;
        }

        _player.Gold.Subtract(_coreList[i].Gold);
        _player.Gold.UpdateView();

        _coreList[i].LvUp();
        photonView.RPC("SyncCoreLvUp", PhotonTargets.Others, i);
        UpdateView(i, _coreList[i].Level);

        var effect = PhotonNetwork.Instantiate("Prefabs/CoreLevelUpEffect/Core" + i.ToString() + "LevelUpEffect", Vector3.zero, Quaternion.identity, 0).GetComponent<ParentSetter>() as ParentSetter;
        effect.SetParent(_player.PhotonView.viewID);

        // Hp Core
        if (i == 1) {
          _player.Hp.UpdateMaxHp();
          _player.Observer.SyncMaxHp();

          _player.Hp.UpdateView();
          _player.Observer.SyncUpdateHpView();
        }

        // Heal Core
        if (i == 4)
          _playerHealer.UpdateMaxHealQuantity();

        _reconfirmList[i] = false;
      }
    }

    public void Init(CorePanel corePanel) {
      _corePanel = corePanel;
    }

    private void UpdateView(int index, int level) {
      _corePanel.ChartList[index].UpdateView(level);
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

    [PunRPC]
    private void SyncCoreLvUp(int index) {
      _coreList[index].LvUp();
    }

    [SerializeField] private BattlePlayer _player;
    [SerializeField] private PlayerAutomaticHealer _playerHealer;
    [SerializeField] private List<Core> _coreList;
    [SerializeField] private List<GameObject> _effectList;
    private CorePanel _corePanel;
    private List<bool> _reconfirmList;
    private static readonly int MaxCoreLevel = 5;
  }
}

