using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BattleApplication : Photon.PunBehaviour {
  void Start() {
    if (PhotonNetwork.player.IsMasterClient)
      _isMaster = true;

    _playerList = new List<PhotonPlayer>();
    _apply.SetActive(true);
    _nameBoard.SetActive(false);
  }

  [PunRPC]
  public void UpdateWaitingList(PhotonPlayer player, string option) {
    if (option == "Add")
      _playerList.Add(player);
    else if (option == "Remove")
      _playerList.Remove(player);

    UpdateNameBoard();
  }

  public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
    _playerList.Remove(player);
    UpdateNameBoard();
  }

  public override void OnPhotonPlayerPropertiesChanged(object[] args) {
    if (_isMaster) {
      var player = args[0] as PhotonPlayer;
      var props = args[1] as Hashtable;
      string option;

      if ((bool)props["Applying"]) {
        _playerList.Add(player);
        option = "Add";
      } else {
        _playerList.Remove(player);
        option = "Remove";
      }

      photonView.RPC("UpdateWaitingList", PhotonTargets.Others, player, option);
      UpdateNameBoard();
    }
  }

  public void Apply() {
    _apply.SetActive(false);
    _nameBoard.SetActive(true);

    var props = new Hashtable() {{"Applying", true}};
    PhotonNetwork.player.SetCustomProperties(props, null, false);
  }

  public void UpdateNameBoard() {
    int length = _playerList.Count;

    for (int i=0; i<length; ++i)
      _textList[i].text = _playerList[i].NickName;

    for (int i=length; i<6; ++i)
      _textList[i].text = "";
  }

  [SerializeField] private GameObject _apply;
  [SerializeField] private GameObject _nameBoard;
  [SerializeField] private List<Text> _textList;
  private bool _isMaster;
  private List<PhotonPlayer> _playerList;
}

/*
public void CreateRoom (Hashtable properties) {
  RoomOptions roomOptions = new RoomOptions ();
  roomOptions.isVisible = true;
  roomOptions.isOpen = true;
  roomOptions.maxPlayers = 4;
  roomOptions.customRoomProperties = new Hashtable (){{"Rank", (string)properties["Rank"]} };
  roomOptions.customRoomPropertiesForLobby = new string[] {"Rank"};

  if (PhotonNetwork.GetRoomList ().Length == 0) {
    PhotonNetwork.CreateRoom ((string)properties ["RoomName"], roomOptions, null);
    return;
  }

  foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList()) {
    if (roomInfo.name != (string)properties ["RoomName"])
      PhotonNetwork.CreateRoom ((string)properties ["RoomName"], roomOptions, null);
    else
      isRoomEnabled = true;
  }
}

public void JoinRoom (Hashtable properties) {
  if (PhotonNetwork.GetRoomList ().Length == 0) {
    isRoomNothing = true;
    return;
  }

  foreach (RoomInfo roomInfo in PhotonNetwork.GetRoomList ()) {
    Hashtable cp = roomInfo.customProperties;

    if (roomInfo.name == (string)properties ["RoomName"]) {
      if ((string)properties ["Rank"] == (string)cp ["Rank"])
        PhotonNetwork.JoinRoom ((string)properties ["RoomName"]);
      else
        isLevelUnmatch = true;

      return;
    }
  }

  isRoomNothing = true;
}

*/
