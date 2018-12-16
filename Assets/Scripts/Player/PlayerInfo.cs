using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerInfo {
    public PlayerInfo(Player player) {
      Team = (int)player.PhotonView.owner.CustomProperties["Team"];
    }

    public int Team { get; private set; }
  }
}

