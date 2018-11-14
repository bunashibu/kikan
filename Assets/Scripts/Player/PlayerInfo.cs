using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerInfo {
    public PlayerInfo(Player player) {
      _player = player;
      _team = (int)PhotonNetwork.player.CustomProperties["Team"];
    }

    public int Team {
      get {
        return _team;
      }
    }

    /*                                                            *
     * INFO: ForceSyncXXX method must be called ONLY by Observer. *
     *       Otherwise it breaks encapsulation.                   *
     *                                                            */
    public void ForceSync(int team) {
      Assert.IsTrue(_player.Observer.ShouldSync("Team"));
      _team = team;
    }

    private Player _player;
    private int _team;
  }
}

