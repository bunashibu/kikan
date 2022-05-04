using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace Bunashibu.Kikan {
  public class JobPanel : Photon.MonoBehaviour {
    void Awake() {
      _decideButton.onClick.AddListener( () => DecideRequest() );
      _selectTimePanel.SetTime(_selectTime);
      _selectTimePanel.SetView(TimeViewType.Sec);
    }

    void Start() {
      EnableAllButtons();

      if (PhotonNetwork.player.IsMasterClient) {
        MonoUtility.Instance.DelayUntil(() => _selectTimePanel.TimeSec <= 0, () => {
          Setup();
          photonView.RPC("StartGameRPC", PhotonTargets.All);
        });
      }
    }

    [PunRPC]
    private void StartGameRPC() {
      ActivatePlayer();
      Destroy(gameObject);
    }

    private void Setup() {
      List<PhotonPlayer> redPlayers = new();
      List<PhotonPlayer> bluePlayers = new();

      foreach (PhotonPlayer player in PhotonNetwork.playerList) {
        int team = (int)player.CustomProperties["Team"];

        if (team == 0)
          redPlayers.Add(player);
        else if (team == 1)
          bluePlayers.Add(player);
      }

      var restRedPlayers = redPlayers.Where(player => !_redPickedPlayers.Contains(player)).ToList();

      var pool = Enumerable.Range(0, _jobs.Length).ToList();
      pool.RemoveAll(x => _redPicks.Contains(x));

      for (int i = 0; i < restRedPlayers.Count; ++i) {
        int index = Random.Range(0, pool.Count);
        int fallbackPick = pool[index];
        photonView.RPC("SyncSetupFallbackRPC", PhotonTargets.All, fallbackPick, restRedPlayers[i]);

        pool.Remove(fallbackPick);
      }

      var restBluePlayers = bluePlayers.Where(player => !_bluePickedPlayers.Contains(player)).ToList();

      pool = Enumerable.Range(0, _jobs.Length).ToList();
      pool.RemoveAll(x => _bluePicks.Contains(x));

      for (int i = 0; i < restBluePlayers.Count; ++i) {
        int index = Random.Range(0, pool.Count);
        int fallbackPick = pool[index];
        photonView.RPC("SyncSetupFallbackRPC", PhotonTargets.All, fallbackPick, restBluePlayers[i]);

        pool.Remove(fallbackPick);
      }
    }

    [PunRPC]
    private void SyncSetupFallbackRPC(int fallbackPick, PhotonPlayer player) {
      if (PhotonNetwork.player == player)
        _fallbackPick = fallbackPick;
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

    [PunRPC]
    private void SyncDecideRPC(int n, int team, PhotonPlayer selectPlayer) {
      if (PhotonNetwork.player == selectPlayer)
        DisableAllButtons();

      if ((int)PhotonNetwork.player.CustomProperties["Team"] == team)
        _decideMark[n].Put();
    }

    [PunRPC]
    private void DecideRequestRPC(int n, int team, PhotonPlayer selectPlayer) {
      if (team == 0) {
        if (_redPicks.Contains(n))
          return;

        _redPicks.Add(n);
        _redPickedPlayers.Add(selectPlayer);
      }
      if (team == 1) {
        if (_bluePicks.Contains(n))
          return;

        _bluePicks.Add(n);
        _bluePickedPlayers.Add(selectPlayer);
      }

      photonView.RPC("SyncDecideRPC", PhotonTargets.All, n, team, selectPlayer);
    }

    private void DecideRequest() {
      if (_curPick == -1)
        return;

      int team = (int)PhotonNetwork.player.CustomProperties["Team"];
      photonView.RPC("DecideRequestRPC", PhotonTargets.MasterClient, _curPick, team, PhotonNetwork.player);
    }

    private void ActivatePlayer() {
      if (_decideButton.interactable)
        _curPick = _fallbackPick;

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
    private int _fallbackPick;

    // Only used by server
    private List<int> _redPicks = new();
    private List<int> _bluePicks = new();
    private List<PhotonPlayer> _redPickedPlayers = new();
    private List<PhotonPlayer> _bluePickedPlayers = new();
  }
}
