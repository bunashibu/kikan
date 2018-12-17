using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerInfo {
    public PlayerInfo(Player player) {
      Team = (int)player.PhotonView.owner.CustomProperties["Team"];
      Name = player.PhotonView.owner.NickName.Trim();
    }

    public int Team { get; private set; }
    public string Name { get; private set; }
  }
}

