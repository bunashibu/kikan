using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class KillDeathPanel : Photon.MonoBehaviour {
    void Update() {
      if (_initFlag) {
        int playerNum = PhotonNetwork.playerList.Length;
        int roomPlayerNum =(int)PhotonNetwork.room.CustomProperties["PlayerNum"];

        if (playerNum == roomPlayerNum) {
          Init();
          _initFlag = false;
        }
      }
    }

    private void Init() {
      _teamTable = new Dictionary<PhotonPlayer, int[]>();

      int index = 0;
      int blueIndex = 0;
      int redIndex = 0;
      foreach (var player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0) {
          index = redIndex;
          ++redIndex;
        } else if (team == 1) {
          index = blueIndex;
          ++blueIndex;
        }

        _teamTable[player] = new int[] { team, index };

        _teamPanels[team].UpdateNameView(player.NickName, index);
      }
    }

    [PunRPC]
    private void SyncKDPanelName(PhotonPlayer player) {
      int team = (int)_teamTable[player][0];
      int index = (int)_teamTable[player][1];
      _teamPanels[team].UpdateNameView(player.NickName, index);
    }

    [PunRPC]
    private void SyncKDPanelLv(int lv, PhotonPlayer player) {
      int team = (int)_teamTable[player][0];
      int index = (int)_teamTable[player][1];
      _teamPanels[team].UpdateLvView(lv, index);
    }

    [PunRPC]
    private void SyncKDPanelKill(int kill, PhotonPlayer player) {
      int team = (int)_teamTable[player][0];
      int index = (int)_teamTable[player][1];
      _teamPanels[team].UpdateKillView(kill, index);
    }

    [PunRPC]
    private void SyncKDPanelDeath(int death, PhotonPlayer player) {
      int team = (int)_teamTable[player][0];
      int index = (int)_teamTable[player][1];
      _teamPanels[team].UpdateDeathView(death, index);
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

    [SerializeField] private KillDeathTeamPanel[] _teamPanels;
    private Dictionary<PhotonPlayer, int[]> _teamTable;
    private bool _initFlag = true;
  }
}

