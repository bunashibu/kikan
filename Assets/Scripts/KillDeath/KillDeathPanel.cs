using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class KillDeathPanel : Photon.PunBehaviour {
    void Start() {
      _playerCellInfo = new Dictionary<PhotonPlayer, CellInfo>();
      UpdatePanel();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
      UpdatePanel();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      UpdatePanel();
    }

    public void UpdatePanel() {
      var redPlayerList = GetTeamPlayerList(0);
      UpdateTeamPanel(0, redPlayerList);

      var bluePlayerList = GetTeamPlayerList(1);
      UpdateTeamPanel(1, bluePlayerList);
    }

    private List<PhotonPlayer> GetTeamPlayerList(int targetTeam) {
      var playerList = new List<PhotonPlayer>();

      foreach (var player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == targetTeam)
          playerList.Add(player);
      }

      return playerList;
    }

    private void UpdateTeamPanel(int team, List<PhotonPlayer> playerList) {
      int index = 0;

      foreach (var player in playerList) {
        _playerCellInfo[player] = new CellInfo() {
          team = team,
          index = index
        };

        _teamPanels[team].UpdateNameView(player.NickName, index);

        int viewID = (int)player.CustomProperties["ViewID"];
        var view = PhotonView.Find(viewID);

        if (view != null) {
          var battlePlayer = view.gameObject.GetComponent<BattlePlayer>();

          _teamPanels[team].UpdateLvView(battlePlayer.Level.Lv, index);
          _teamPanels[team].UpdateKillView(battlePlayer.KillDeath.KillCount, index);
          _teamPanels[team].UpdateDeathView(battlePlayer.KillDeath.DeathCount, index);
        }

        ++index;
      }

      for (int i = index; i < 3; ++i) {
        _teamPanels[team].UpdateNameView("", i);
        _teamPanels[team].UpdateLvView(-1, i);
        _teamPanels[team].UpdateKillView(-1, i);
        _teamPanels[team].UpdateDeathView(-1, i);
      }
    }

    public void UpdateNameView(PhotonPlayer player) {
      photonView.RPC("SyncKDPanelName", PhotonTargets.All, player);
    }

    public void UpdateLvView(int lv) {
      photonView.RPC("SyncKDPanelLv", PhotonTargets.All, lv, PhotonNetwork.player);
    }

    public void UpdateKillView(int kill, PhotonPlayer player) {
      photonView.RPC("SyncKDPanelKill", PhotonTargets.All, kill, player);
    }

    public void UpdateDeathView(int death, PhotonPlayer player) {
      photonView.RPC("SyncKDPanelDeath", PhotonTargets.All, death, player);
    }

    [PunRPC]
    private void SyncKDPanelName(PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateNameView(player.NickName, index);
    }

    [PunRPC]
    private void SyncKDPanelLv(int lv, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateLvView(lv, index);
    }

    [PunRPC]
    private void SyncKDPanelKill(int kill, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateKillView(kill, index);
    }

    [PunRPC]
    private void SyncKDPanelDeath(int death, PhotonPlayer player) {
      int team = _playerCellInfo[player].team;
      int index = _playerCellInfo[player].index;
      _teamPanels[team].UpdateDeathView(death, index);
    }

    [SerializeField] private KillDeathTeamPanel[] _teamPanels;
    private Dictionary<PhotonPlayer, CellInfo> _playerCellInfo;
  }

  public class CellInfo {
    public int team;
    public int index;
  }
}

