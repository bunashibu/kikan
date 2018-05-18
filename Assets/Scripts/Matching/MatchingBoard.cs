using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class MatchingBoard : Photon.PunBehaviour {
    void Start() {
      SetApplyMode();
    }

    public void SetApplyMode() {
      _apply.SetActive(true);

      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    public void SetMatchWaitingMode() {
      _cancel.SetActive(true);
      _nameBoard.SetActive(true);
      _progressLabel.SetActive(true);

      _apply.SetActive(false);
    }

    public void SetStartBattleMode() {
      _startPanel.SetActive(true);

      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      //_logout.SetActive(false);
    }

    public void UpdateNameBoard() {
      var applyType = (ApplyType)PhotonNetwork.player.CustomProperties["ApplyingTicket"];
      var playerList = _mediator.ApplicantList[applyType];

      for (int i=0; i<playerList.Count; ++i)
        _boardNameList[i].text = playerList[i].NickName;

      int matchCount = _mediator.MatchCount[applyType];
      for (int i=playerList.Count; i<matchCount; ++i)
        _boardNameList[i].text = "";
    }

    public void CleanNameBoard() {
      for (int i=0; i<_mediator.MatchCount[ApplyType.VS3]; ++i)
        _boardNameList[i].text = "";
    }

    [SerializeField] private GameObject _apply;
    [SerializeField] private GameObject _cancel;
    [SerializeField] private GameObject _nameBoard;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private List<Text> _boardNameList;
    [SerializeField] private MatchingMediator _mediator;
  }
}

