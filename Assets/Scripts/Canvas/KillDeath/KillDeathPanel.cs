using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class KillDeathPanel : Photon.PunBehaviour {
    void Start() {
      _playerCellInfo = new Dictionary<PhotonPlayer, CellInfo>();
      UpdatePlayerCell();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
      UpdatePlayerCell();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      //UpdatePlayerCell();
    }

    public void UpdateLevel(int level, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateLvView(level, index);
    }

    public void UpdateKill(int killCount, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateKillView(killCount, index);
    }

    public void UpdateDeath(int deathCount, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateDeathView(deathCount, index);
    }

    private void UpdatePlayerCell() {
      int index = 0;
      int redIndex = 0;
      int blueIndex = 0;

      foreach (var player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0) {
          index = redIndex;
          ++redIndex;
        }
        else if (team == 1) {
          index = blueIndex;
          ++blueIndex;
        }

        _playerCellInfo[player] = new CellInfo() {
          team = team,
          index = index
        };
        _teamPanels[team].UpdateNameView(player.NickName, index);
      }
    }

    [SerializeField] private KillDeathTeamPanel[] _teamPanels;
    private Dictionary<PhotonPlayer, CellInfo> _playerCellInfo;
  }

  public class CellInfo {
    public int team;
    public int index;
  }
}

