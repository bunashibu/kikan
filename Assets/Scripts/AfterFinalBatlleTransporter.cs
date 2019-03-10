using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class AfterFinalBatlleTransporter : Photon.PunBehaviour {
    public override void OnConnectedToMaster() {
      PhotonNetwork.JoinRoom("Lobby");
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      RoomOptions roomOptions = new RoomOptions();
      roomOptions.MaxPlayers = (byte)20;

      PhotonNetwork.JoinOrCreateRoom("Lobby", roomOptions, null);
    }

    public override void OnJoinedRoom() {
      SceneChanger.Instance.ChangeScene("Lobby");
    }

    public override void OnLeftRoom() {
      if (PhotonNetwork.connected)
        PhotonNetwork.JoinRoom("Lobby");
      else
        PhotonNetwork.ConnectUsingSettings(GameData.Instance.GameVersion);
    }
  }
}

