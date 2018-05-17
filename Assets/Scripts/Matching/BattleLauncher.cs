using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class BattleLauncher : Photon.PunBehaviour {
    public void StartBattle(ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var tmp = PhotonNetwork.room.CustomProperties["Playing"];
      int roomNum = 0;
      if (tmp != null)
        roomNum = (int)tmp;

      var props = new Hashtable() {{ "Playing", roomNum + 1 }};
      PhotonNetwork.room.SetCustomProperties(props);

      var roomName = "Battle" + roomNum.ToString();
      int[] team = TeamMaker(_mediator.MatchCount[applyType]);

      photonView.RPC("StartBattleRPC", PhotonTargets.AllViaServer, roomName, team, applyType);
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

    [PunRPC]
    public void StartBattleRPC(string roomName, int[] team, ApplyType applyType) {
      var applyingTicket = PhotonNetwork.player.CustomProperties["ApplyingTicket"];
      if (applyingTicket == null)
        return;

      if ((ApplyType)applyingTicket == applyType)
        _isApplying = true;

      if (_isApplying) {
        _board.SetStartBattleMode();
        _roomName = roomName;

        var playerList = _mediator.ApplicantList[applyType];

        for (int i=0; i<playerList.Count; ++i) {
          if (playerList[i] == PhotonNetwork.player) {
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

    public override void OnConnectedToMaster() {
      if (_isApplying) {
        var applyType = (ApplyType)PhotonNetwork.player.CustomProperties["ApplyingTicket"];

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)_mediator.MatchCount[applyType];
        roomOptions.CustomRoomProperties = new Hashtable() {{ "PlayerNum", _mediator.MatchCount[applyType] }};

        PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, null);
      }
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      if (_isApplying) {
        MonoUtility.Instance.DelaySec(1.0f, () => {
          PhotonNetwork.JoinRoom(_roomName);
        });
      }
    }

    public override void OnJoinedRoom() {
      if (_isApplying)
        SceneChanger.Instance.ChangeScene("Battle");
    }

    [SerializeField] private MatchingBoard _board;
    [SerializeField] private MatchingMediator _mediator;
    [SerializeField] private int _countDown;
    [SerializeField] private Text _CountDown;
    private bool _isApplying;
    private string _roomName;
  }
}

