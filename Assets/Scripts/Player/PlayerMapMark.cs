using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerMapMark : MonoBehaviour {
    void Start() {
      if (_player.PhotonView.owner == PhotonNetwork.player) {
        _renderer.color = Color.yellow;
        return;
      }

      int playerTeam = (int)_player.PhotonView.owner.CustomProperties["Team"];
      int clientTeam = (int)PhotonNetwork.player.CustomProperties["Team"];
      if (playerTeam == clientTeam)
        _renderer.color = new Color(1.0f, 0.45f, 0.0f, 1.0f); // Orange
      else
        _renderer.color = Color.red;
    }

    [SerializeField] private BattlePlayer _player;
    [SerializeField] private SpriteRenderer _renderer;
  }
}

