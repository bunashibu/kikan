using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class KillMessagePanel : MonoBehaviour {
    public void InstantiateMessage(Player killPlayer, Player deathPlayer, bool isSameTeam) {
      var killMessage = Instantiate(_killMessagePref, transform).GetComponent<KillMessage>();

      killMessage.SetKillPlayerName(killPlayer.PlayerInfo.Name, isSameTeam);
      killMessage.SetDeathPlayerName(deathPlayer.PlayerInfo.Name);
    }

    [SerializeField] private GameObject _killMessagePref;
  }
}

