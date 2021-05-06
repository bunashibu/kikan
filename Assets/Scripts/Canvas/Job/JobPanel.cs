﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Bunashibu.Kikan {
  public class JobPanel : Photon.MonoBehaviour {
    void Awake() {
      _decideButton.onClick.AddListener( () => SyncDecide() );
      _selectTimePanel.SetTime(_selectTime);
      _selectTimePanel.SetView(TimeViewType.Sec);
    }

    void Start() {
      EnableAllButtons();

      MonoUtility.Instance.DelayUntil(() => _selectTimePanel.TimeSec <= 0, () => {
        ActivatePlayer();

        Destroy(gameObject);
      });
    }

    public void Pick(int n) {
      if (_curPick == -1) {
        _curPick = n;
        _buttons[_curPick].interactable = false;
      }
      else {
        _prePick = _curPick;
        _curPick = n;
        _buttons[_prePick].interactable = true;
        _buttons[_curPick].interactable = false;
      }

      _description.UpdateLabel(n);
    }

    private void RandomPick() {
      int n = Random.Range(0, _jobs.Length);
      Pick(n);
    }

    [PunRPC]
    private void SyncDecideRPC(int n, int team) {
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == team)
        _decideMark[n].Put();
    }

    private void SyncDecide() {
      if (_curPick == -1)
        return;

      int team = (int)PhotonNetwork.player.CustomProperties["Team"];
      photonView.RPC("SyncDecideRPC", PhotonTargets.All, _curPick, team);
      DisableAllButtons();
    }

    private void ActivatePlayer() {
      if (_decideButton.interactable)
        RandomPick();

      var player = _instantiator.InstantiatePlayer(_jobs[_curPick].name);
      _instantiator.InstantiateHudObjects(_canvas, player.Weapon, _skillPanelList[_curPick]);

      _eventSynchronizer.SyncPlayerInitialize(player.PhotonView.viewID);
    }

    private void EnableAllButtons() {
      foreach (Button button in _buttons) {
        button.interactable = true;
        button.gameObject.GetComponent<EventTrigger>().enabled = true;
      }

      _decideButton.interactable = true;
      _decideButton.gameObject.GetComponent<EventTrigger>().enabled = true;
    }

    private void DisableAllButtons() {
      foreach (Button button in _buttons) {
        button.interactable = false;
        button.gameObject.GetComponent<EventTrigger>().enabled = false;
      }

      _decideButton.interactable = false;
      _decideButton.gameObject.GetComponent<EventTrigger>().enabled = false;
    }

    [SerializeField] private StartUpInstantiator _instantiator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _decideButton;
    [SerializeField] private JobDescription _description;
    [SerializeField] private TimePanel _selectTimePanel;
    [SerializeField] private EventSynchronizer _eventSynchronizer;

    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private JobDecideMark[] _decideMark;

    // In order to avoid setting wrong index on inspector,
    // _skillPanelList is here. not in _instantiator.
    [SerializeField] private List<SkillPanel> _skillPanelList;

    [Header("Job Select Time (Second)")]
    [SerializeField] private float _selectTime;

    private int _prePick = -1;
    private int _curPick = -1;
  }
}
