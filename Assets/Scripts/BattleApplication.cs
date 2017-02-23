using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BattleApplication : Photon.PunBehaviour {
  void Start() {
    if (PhotonNetwork.player.IsMasterClient)
      _isMaster = true;

    _apply.SetActive(true);
    _nameBoard.SetActive(false);
  }

  public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
    if (_isMaster) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];

      var list = new List<string>();
      if (playerNames != null)
        list.AddRange(playerNames);
      list.Remove(player.NickName);

      var props = new Hashtable() {{"Applying", list.ToArray()}};
      PhotonNetwork.room.SetCustomProperties(props);

      photonView.RPC("UpdateNameBoard", PhotonTargets.All);
    }
  }

  public void Apply() {
    _apply.SetActive(false);
    _nameBoard.SetActive(true);

    var player = PhotonNetwork.player;
    photonView.RPC("Approve", PhotonTargets.MasterClient, player);
  }

  [PunRPC]
  public void Approve(PhotonPlayer player) {
    if (_isMaster) {
      var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];

      var list = new List<string>();
      if (playerNames != null)
        list.AddRange(playerNames);
      list.Add(player.NickName);

      var props = new Hashtable() {{"Applying", list.ToArray()}};
      PhotonNetwork.room.SetCustomProperties(props);

      photonView.RPC("UpdateNameBoard", PhotonTargets.All);
    }
  }

  [PunRPC]
  public void UpdateNameBoard() {
    var playerNames = PhotonNetwork.room.CustomProperties["Applying"] as string[];

    var list = new List<string>();
    if (playerNames != null)
      list.AddRange(playerNames);
    int length = list.Count;
    Debug.Log(length);

    for (int i=0; i<length; ++i)
      _nameList[i].text = list[i];

    for (int i=length; i<6; ++i)
      _nameList[i].text = "";
  }

  [SerializeField] private GameObject _apply;
  [SerializeField] private GameObject _nameBoard;
  [SerializeField] private List<Text> _nameList;
  private bool _isMaster;
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
