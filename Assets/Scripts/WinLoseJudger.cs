using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class WinLoseJudger : Photon.PunBehaviour {
    void Start() {
      InitAlivePlayerCount();
      MonoUtility.Instance.DelayUntil(() => _timePanel.TimeSec <= 0, () => {
        int[] alivePlayerCount = PhotonNetwork.room.CustomProperties["AlivePlayerCount"] as int[];
        int redCount = alivePlayerCount[0];
        int blueCount = alivePlayerCount[1];

        if ( (redCount > 0) && (blueCount > 0) )
          ShowDraw();
      });
    }

    public void SetTimePanel(TimePanel timePanel) {
      _timePanel = timePanel;
    }

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable props) {
      int[] alivePlayerCount = props["AlivePlayerCount"] as int[];
      int redCount = alivePlayerCount[0];
      int blueCount = alivePlayerCount[1];

      if ( (redCount > 0) && (blueCount > 0) )
        return;

      int team = (int)PhotonNetwork.player.CustomProperties["Team"];

      if ( (redCount == 0 && team == 0) || (blueCount == 0 && team == 1) )
        ShowLose();
      else
        ShowWin();
    }

    private void InitAlivePlayerCount() {
      int redCount = 0;
      int blueCount = 0;

      foreach (PhotonPlayer player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0)
          redCount += 1;
        else if (team == 1)
          blueCount += 1;
      }

      var props = new Hashtable() {{ "AlivePlayerCount", new int[] {redCount, blueCount} }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    private void ShowWin() {
      Debug.Log("Win");
    }

    private void ShowLose() {
      Debug.Log("Lose");
    }

    private void ShowDraw() {
      Debug.Log("Draw");
    }

    private TimePanel _timePanel;
  }
}

