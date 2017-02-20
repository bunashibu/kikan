using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class BattleApplication : MonoBehaviour {
  void Start() {
    _apply.SetActive(true);
    _nameBoard.SetActive(false);
  }

  void Update() {
    /*
    PhotonNetwork.CreateRoom("Battle", new RoomOptions() { MaxPlayers = 6 }, null);
    PhotonNetwork.LeaveRoom();
    */
  }

  public void Apply() {
    _apply.SetActive(false);
    _nameBoard.SetActive(true);

    var players = PhotonNetwork.room.CustomProperties["Matching"] as PhotonPlayer[];

    var list = new List<PhotonPlayer>();
    if (players != null) {
      Debug.Log("Get exisiting matching players");
      foreach (var player in players)
        list.Add(player);
    }
    list.Add(PhotonNetwork.player);
    int lastIndex = list.Count - 1;

    var properties = new Hashtable() {{"Matching", list.ToArray()}};

    PhotonNetwork.room.SetCustomProperties(properties, null, false);

    _textList[lastIndex].text = PhotonNetwork.player.NickName;

    Debug.Log(PhotonNetwork.room.CustomProperties);
    var matching = (PhotonPlayer[])PhotonNetwork.room.CustomProperties["Matching"];
    Debug.Log(matching);
    Debug.Log(matching[0]);
    Debug.Log(matching.Length);
  }

  [SerializeField] private GameObject _apply;
  [SerializeField] private GameObject _nameBoard;
  [SerializeField] private Text[] _textList;
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
