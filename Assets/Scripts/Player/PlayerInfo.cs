using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerInfo {
    public int Team {
      get {
        return (int)PhotonNetwork.player.CustomProperties["Team"];
      }
    }
  }
}

