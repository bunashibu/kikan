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

    public override void OnPhotonCustomRoomPropertiesChanged(Hashtable props) {
      if (_isApplying)
        UpdateNameBoard();
    }

    public void SetApplyMode() {
      _isApplying = false;
      _apply.SetActive(true);

      _cancel.SetActive(false);
      _nameBoard.SetActive(false);
      _progressLabel.SetActive(false);
      _startPanel.SetActive(false);
    }

    public void SetApproveWaitingMode() {
      _apply.SetActive(false);
    }

    public void SetMatchWaitingMode() {
      _isApplying = true;
      _cancel.SetActive(true);
      _nameBoard.SetActive(true);
      _progressLabel.SetActive(true);

      _apply.SetActive(false);

      UpdateNameBoard();
    }

    private void UpdateNameBoard() {
      var applyingTicket = (int)PhotonNetwork.player.CustomProperties["ApplyingTicket"];
      string propKey = "Applying" + applyingTicket.ToString();

      var playerNameAry  = PhotonNetwork.room.CustomProperties[propKey] as string[];
      var playerNameList = MonoUtility.ToList<string>(playerNameAry);

      for (int i=0; i<playerNameList.Count; ++i)
        _boardNameList[i].text = playerNameList[i];

      int matchCount = _mediator.MatchCount[(ApplyType)applyingTicket];
      for (int i=playerNameList.Count; i<matchCount; ++i)
        _boardNameList[i].text = "";
    }

    [SerializeField] private GameObject _apply;
    [SerializeField] private GameObject _cancel;
    [SerializeField] private GameObject _nameBoard;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private List<Text> _boardNameList;
    [SerializeField] private MatchingMediator _mediator;
    private bool _isApplying;
  }
}

