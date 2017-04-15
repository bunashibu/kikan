using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BattleApplication : Photon.PunBehaviour {
  void Start() {
    if (PhotonNetwork.player.IsMasterClient)
      _isMaster = true;

    _isApplying = false;
    _apply.SetActive(true);
    _nameBoard.SetActive(false);
    _progressLabel.SetActive(false);
    _startPanel.SetActive(false);
  }

  public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
    if (_isMaster) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      list.Remove(player.NickName);

      var props = new Hashtable() {{"Applying", list.ToArray()}};
      PhotonNetwork.room.SetCustomProperties(props);

      photonView.RPC("UpdateNameBoard", PhotonTargets.All);
    }
  }

  public override void OnConnectedToMaster() {
    if (_isApplying) {
      Debug.Log("OnConnectedToMaster() BA was called");

      if (_isMaster) {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)_matchNum;

        PhotonNetwork.CreateRoom(_roomName, roomOptions, null);
      } else
        PhotonNetwork.JoinRoom(_roomName);
    }
  }

  public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
    if (_isApplying) {
      Debug.Log("OnPhotonJoinRoomFailed() BA was called");

      MonoUtility.Instance.DelaySec(1.0f, () => {
        PhotonNetwork.JoinRoom(_roomName);
      });
    }
  }

  public override void OnJoinedRoom() {
    if (_isApplying) {
      Debug.Log("OnJoinedRoom() BA was called");
      _sceneChanger.ChangeScene("Battle");
    }
  }

  public void Apply() {
    _isApplying = true;
    _apply.SetActive(false);
    _nameBoard.SetActive(true);
    _progressLabel.SetActive(true);

    var player = PhotonNetwork.player;
    photonView.RPC("Approve", PhotonTargets.MasterClient, player);
  }

  [PunRPC]
  public void Approve(PhotonPlayer player) {
    if (_isMaster) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
      var list = MonoUtility.ToList<string>(playerNames);

      list.Add(player.NickName);

      var props = new Hashtable() {{"Applying", list.ToArray()}};
      PhotonNetwork.room.SetCustomProperties(props);

      photonView.RPC("UpdateNameBoard", PhotonTargets.All);

      if (list.Count == _matchNum) {
        Debug.Log("Matching is done. Prepareing to start game...");

        var tmp = PhotonNetwork.room.CustomProperties["Playing"];
        int roomNum = 0;
        if (tmp != null)
          roomNum = (int)tmp;

        props = new Hashtable() {{"Playing", roomNum + 1}};
        PhotonNetwork.room.SetCustomProperties(props);

        var roomName = "Battle" + roomNum.ToString();

        int[] team = TeamMaker();
        photonView.RPC("StartBattle", PhotonTargets.AllViaServer, roomName, team);
      }
    }
  }

  [PunRPC]
  public void UpdateNameBoard() {
    var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];
    var list = MonoUtility.ToList<string>(playerNames);

    int length = list.Count;

    for (int i=0; i<length; ++i)
      _nameList[i].text = list[i];

    for (int i=length; i<_matchNum; ++i)
      _nameList[i].text = "";
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
          var props = new Hashtable() {{"Team", team[i]}};
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

  [SerializeField] private GameObject _apply;
  [SerializeField] private GameObject _nameBoard;
  [SerializeField] private GameObject _progressLabel;
  [SerializeField] private GameObject _startPanel;
  [SerializeField] private GameObject _logout;
  [SerializeField] private Text _CountDown;
  [SerializeField] private List<Text> _nameList;
  [SerializeField] private int _matchNum;
  [SerializeField] private SceneChanger _sceneChanger;
  [SerializeField] private int _countDown; // Debug
  private bool _isMaster;
  private bool _isApplying;
  private string _roomName;
}

