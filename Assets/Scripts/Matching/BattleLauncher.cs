using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class BattleLauncher : Photon.MonoBehaviour {
    public void StartBattle(int matchCount) {
      var tmp = PhotonNetwork.room.CustomProperties["Playing"];
      int roomNum = 0;
      if (tmp != null)
        roomNum = (int)tmp;

      var props = new Hashtable() {{ "Playing", roomNum + 1 }};
      PhotonNetwork.room.SetCustomProperties(props);

      var roomName = "Battle" + roomNum.ToString();
      int[] team = TeamMaker(matchCount);

      photonView.RPC("StartBattleRPC", PhotonTargets.AllViaServer, roomName, team);
    }

    [PunRPC]
    public void StartBattleRPC(string roomName, int[] team) {
      if (_isApplying) {
        /*
        _nameBoard.SetActive(false);
        _progressLabel.SetActive(false);
        _logout.SetActive(false);
        _startPanel.SetActive(true);
        */

        _roomName = roomName;

        foreach (var x in team)
          Debug.Log(x);

        var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
        var list = MonoUtility.ToList<string>(playerNames);

        for (int i=0; i<list.Count; ++i) {
          if (list[i] == PhotonNetwork.player.NickName) {
            var props = new Hashtable() {{ "Team", team[i] }};
            PhotonNetwork.player.SetCustomProperties(props);
            break;
          }
        }

        CountDown(_countDown);
      }
    }

    private void CountDown(int cnt) {
      _CountDown.text = cnt.ToString();

      MonoUtility.Instance.DelaySec(1.0f, () => {
        cnt -= 1;

        if (cnt <= 0)
          PhotonNetwork.LeaveRoom();
        else
          CountDown(cnt);
      });
    }

    private int[] TeamMaker(int matchCount) {
      var list = new List<int>();
      int half = 1;

      if (matchCount > 2) {
        half = matchCount / 2;

        if (matchCount % 2 != 0)
          half += 1;
      }

      for (int i=0; i<matchCount; ++i) {
        var num0 = list.Where(x => x == 0).Count();
        var num1 = list.Where(x => x == 1).Count();

        if (num0 < half) {
          if (Random.value < 0.5 || num1 >= half)
            list.Add(0);
          else
            list.Add(1);
        }
        else
          list.Add(1);
      }

      return list.ToArray();
    }

    [SerializeField] private MatchingMediator _mediator;
    [SerializeField] private int _countDown;
    [SerializeField] private Text _CountDown;
    private bool _isApplying;
    private string _roomName;
  }
}

