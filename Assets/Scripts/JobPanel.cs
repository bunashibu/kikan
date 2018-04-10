using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobPanel : Photon.PunBehaviour {
    void Start() {
      DisableAllButtons();
      UpdateMatchPlayerCount();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
      UpdateMatchPlayerCount();
    }

    public void Pick(int n) {
      _playerInstantiator.InstantiatePlayer(_jobs[n].name);
      _playerInstantiator.InstantiateHudObjects(_canvas, _skillPanelList[n]);
      _playerInstantiator.InitAll(_jobStatus[n]);

      DisableAllButtons();

      Destroy(gameObject);
      Destroy(_playerInstantiator.gameObject);
    }

    private void UpdateMatchPlayerCount() {
      int matchPlayerCount = (int)PhotonNetwork.room.CustomProperties["PlayerNum"];

      if (PhotonNetwork.room.playerCount == matchPlayerCount)
        EnableAllButtons();
    }

    private void EnableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = true;
    }

    private void DisableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = false;
    }

    [SerializeField] private PlayerInstantiator _playerInstantiator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private JobStatus[] _jobStatus;
    [SerializeField] private List<SkillPanel> _skillPanelList;

  }
}

