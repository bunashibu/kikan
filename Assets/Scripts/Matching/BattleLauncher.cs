﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class BattleLauncher : Photon.PunBehaviour {
    void Awake() {
      _audioSource = GetComponent<AudioSource>();
    }

    public void StartBattle(ApplyType applyType) {
      Assert.IsTrue(PhotonNetwork.isMasterClient);

      var roomName = "Battle" + System.Guid.NewGuid();
      int[] teams = TeamMaker(_mediator.MatchCount[applyType], applyType);

      photonView.RPC("StartBattleRPC", PhotonTargets.AllViaServer, roomName, teams, applyType);
    }

    private int[] TeamMaker(int matchCount, ApplyType applyType) {
      int teamCount = matchCount == 1 ? 1 : matchCount / 2;
      var playerList = _mediator.ApplicantList[applyType];

      var redIndex = new List<int>();
      var blueIndex = new List<int>();
      var undecidedIndex = new List<int>();

      for (int i=0; i<playerList.Count; ++i) {
        int hopeTeam = (int)playerList[i].CustomProperties["HopeTeam"];

        if (hopeTeam == -1)
          undecidedIndex.Add(i);
        else if (hopeTeam == 0 && redIndex.Count < teamCount)
          redIndex.Add(i);
        else if (hopeTeam == 1 && blueIndex.Count < teamCount)
          blueIndex.Add(i);
        else
          undecidedIndex.Add(i);
      }

      for (int k=0; k<undecidedIndex.Count; ++k) {
        if (redIndex.Count == teamCount)
          blueIndex.Add(undecidedIndex[k]);
        else if (blueIndex.Count == teamCount)
          redIndex.Add(undecidedIndex[k]);
        else if (Random.value < 0.5)
          redIndex.Add(undecidedIndex[k]);
        else
          blueIndex.Add(undecidedIndex[k]);
      }

      var finalAry = new int[6];
      foreach (var i in redIndex)
        finalAry[i] = 0;
      foreach (var i in blueIndex)
        finalAry[i] = 1;

      return finalAry;
    }

    [PunRPC]
    public void StartBattleRPC(string roomName, int[] teams, ApplyType applyType) {
      if (applyType != ApplyType.Practice)
        _audioSource.PlayOneShot(_matchingClip, 0.1f);

      var applyingTicket = PhotonNetwork.player.CustomProperties["ApplyingTicket"];
      if (applyingTicket == null) return;

      if ((ApplyType)applyingTicket == applyType) {
        UseApplyingTicket(applyType);
        _isApplying = true;
      }

      if (_isApplying) {
        _board.SetStartBattleMode();
        _roomName = roomName;

        var playerList = _mediator.ApplicantList[applyType];

        for (int i=0; i<playerList.Count; ++i) {
          if (playerList[i] == PhotonNetwork.player) {

            if (i == 0)
              photonView.RPC("MatchingDoneRPC", PhotonTargets.MasterClient, applyType);

            var props = new Hashtable() {{ "Team", teams[i] }};
            PhotonNetwork.player.SetCustomProperties(props);
            break;
          }
        }

        CountDown(_countDown);
      }
    }

    private void UseApplyingTicket(ApplyType applyType) {
      _applyType = applyType;

      var props = new Hashtable() {{ "ApplyingTicket", "" }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void CountDown(int cnt) {
      _CountDown.text = cnt.ToString();

      MonoUtility.Instance.DelaySec(1.0f, () => {
        cnt -= 1;

        if (cnt <= 0)
          SceneChanger.Instance.FadeOutAndLeaveRoom();
        else
          CountDown(cnt);
      });
    }

    public override void OnConnectedToMaster() {
      if (_isApplying) {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)_mediator.MatchCount[_applyType];
        roomOptions.CustomRoomProperties = new Hashtable() {{ "PlayerNum", _mediator.MatchCount[_applyType] }};

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
    [SerializeField] private AudioClip _matchingClip;
    private ApplyType _applyType;
    private bool _isApplying;
    private string _roomName;
    private AudioSource _audioSource;
  }
}
