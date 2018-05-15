using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class BattleApplication : Photon.PunBehaviour {
    void Start() {
      _isApplying = false;
      _apply.SetActive(true);
      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      if (list.Contains(player.NickName))
        RemoveApplyingPlayer(player);
    }

    public override void OnConnectedToMaster() {
      if (_isApplying) {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)_matchNum;
        roomOptions.CustomRoomProperties = new Hashtable() {{ "PlayerNum", _matchNum }};

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

    /*
    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable props) {
      UpdateNameBoard();
    }
    */

    public void Apply() {
      _isApplying = true;
      _apply.SetActive(false);
      _cancel.SetActive(true);
      _nameBoard.SetActive(true);
      _progressLabel.SetActive(true);

      photonView.RPC("Approve", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    public void Approve(PhotonPlayer player) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      list.Add(player.NickName);

      var props = new Hashtable() {{ "Applying", list.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);

      if (list.Count == _matchNum) {
        Debug.Log("Matching is done. Prepareing to start game...");

        var tmp = PhotonNetwork.room.CustomProperties["Playing"];
        int roomNum = 0;
        if (tmp != null)
          roomNum = (int)tmp;

        props = new Hashtable() {{ "Playing", roomNum + 1 }};
        PhotonNetwork.room.SetCustomProperties(props);

        var roomName = "Battle" + roomNum.ToString();

        int[] team = TeamMaker();
        photonView.RPC("StartBattle", PhotonTargets.AllViaServer, roomName, team);
      }
    }

    [PunRPC]
    public void StartBattle(string roomName, int[] team) {
      if (_isApplying) {
        _nameBoard.SetActive(false);
        _progressLabel.SetActive(false);
        _logout.SetActive(false);
        _startPanel.SetActive(true);

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

    public void CountDown(int cnt) {
      _CountDown.text = cnt.ToString();

      MonoUtility.Instance.DelaySec(1.0f, () => {
        cnt -= 1;

        if (cnt <= 0)
          PhotonNetwork.LeaveRoom();
        else
          CountDown(cnt);
      });
    }

    public void Cancel() {
      RemoveApplyingPlayer(PhotonNetwork.player);

      _isApplying = false;
      _apply.SetActive(true);
      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    private int[] TeamMaker() {
      var list = new List<int>();
      int half = 1;

      if (_matchNum > 2) {
        half = _matchNum / 2;

        if (_matchNum % 2 != 0)
          half += 1;
      }

      for (int i=0; i<_matchNum; ++i) {
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

    private void UpdateNameBoard() {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      int length = list.Count;

      for (int i=0; i<length; ++i)
        _nameList[i].text = list[i];

      for (int i=length; i<_matchNum; ++i)
        _nameList[i].text = "";
    }

    private void RemoveApplyingPlayer(PhotonPlayer player) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      list.Remove(player.NickName);

      var props = new Hashtable() {{ "Applying", list.ToArray() }};
      PhotonNetwork.room.SetCustomProperties(props);
    }

    [SerializeField] private GameObject _apply;
    [SerializeField] private GameObject _cancel;
    [SerializeField] private GameObject _nameBoard;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _logout;
    [SerializeField] private Text _CountDown;
    [SerializeField] private List<Text> _nameList;
    [SerializeField] private int _matchNum;
    [SerializeField] private int _countDown;
    private bool _isApplying;
    private string _roomName;
  }
}

