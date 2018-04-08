using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class JobPicker : MonoBehaviour {
    void Start() {
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        _initialCameraPosition.x *= -1;

      _trackCamera.SetPosition(_initialCameraPosition);
      //Destroy(gameObject, 10.0f);
    }

    public void Pick(int n) {
      InstantiatePlayer(n);
      InstantiateHudObjects(n);

      InitPlayerSettings();

      InitPlayerHp();
      InitPlayerExp();
      InitPlayerLv();
      InitPlayerKillDeath();
      InitPlayerGold();
      InitPlayerCore();
      InitPlayerStatus(n);
      InitPlayerMovement(n);

      DisableAllButtons();
      Destroy(_camera);
      Destroy(gameObject);
    }

    private void InstantiatePlayer(int n) {
      var pos = _stageData.RespawnPosition;

      // INFO: Team 0 is Red(Right), Team 1 is Blue(Left)
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        pos.x *= -1;

      _player = PhotonNetwork.Instantiate("Prefabs/Job/" + _jobs[n].name, pos, Quaternion.identity, 0).GetComponent<BattlePlayer>();
      AdjustFlipX();
      SetViewID();
      _player.Observer.SyncTeam();
    }

    private void InstantiateHudObjects(int n) {
      _hpBar = Instantiate(_hpBar) as Bar;
      _hpBar.transform.SetParent(_canvas.transform, false);

      _expBar = Instantiate(_expBar) as Bar;
      _expBar.transform.SetParent(_canvas.transform, false);

      _lvPanel = Instantiate(_lvPanel) as LevelPanel;
      _lvPanel.transform.SetParent(_canvas.transform, false);

      var skillPanel = Instantiate(_skillPanelList[n]) as SkillPanel;
      skillPanel.transform.SetParent(_canvas.transform, false);

      _player.Weapon.SkillInstantiator.AttachSkillPanel(skillPanel);
    }

    private void AdjustFlipX() {
      var renderers = _player.Renderers;
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1) {
        foreach (var sprite in renderers)
          sprite.flipX = true;
      }
    }

    private void SetViewID() {
      var viewID = _player.PhotonView.viewID;

      var props = new Hashtable() {{"ViewID", viewID}};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void InitPlayerSettings() {
      _trackCamera.SetTrackTarget(_player.gameObject);

      int team = (int)PhotonNetwork.player.CustomProperties["Team"];
      if (team == 0)
        _player.NameBackground.SetColor("RED");
      else if (team == 1)
        _player.NameBackground.SetColor("BLUE");

      _player.Observer.SyncNameBackground();
    }

    private void InitPlayerHp() {
      _player.Hp.AttachHudBar(_hpBar);
      _player.Hp.UpdateView();
      _player.Observer.SyncUpdateHpView();
    }

    private void InitPlayerExp() {
      var playerNextExp = _player.NextExp;
      playerNextExp.Init(_expBar);
      playerNextExp.UpdateView();
    }

    private void InitPlayerLv() {
      var playerLv = _player.Level;
      playerLv.Init(_lvPanel, _kdPanel);
      playerLv.UpdateView();
    }

    private void InitPlayerKillDeath() {
      var playerKDRec = _player.KillDeath;
      playerKDRec.Init(_kdPanel);
    }

    private void InitPlayerGold() {
      var playerGold = _player.Gold;
      playerGold.Init(_goldPanel);
    }

    private void InitPlayerCore() {
      var playerCore = _player.Core;
      playerCore.Init(_corePanel);
    }

    private void InitPlayerStatus(int n) {
      var status = _player.Status;
      status.Init(_jobData[n]);
    }

    private void InitPlayerMovement(int n) {
      _player.Movement.SetMoveForce(_jobData[n].Spd);
      _player.Movement.SetJumpForce(_jobData[n].Jmp);
    }

    private void DisableAllButtons() {
      foreach (Button button in _buttons)
        button.interactable = false;
    }

    [SerializeField] private GameObject[] _jobs;
    [SerializeField] private GameObject _camera;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private JobStatus[] _jobData;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Bar _hpBar;
    [SerializeField] private Bar _expBar;
    [SerializeField] private LevelPanel _lvPanel;
    [SerializeField] private KillDeathPanel _kdPanel;
    [SerializeField] private GoldPanel _goldPanel;
    [SerializeField] private CorePanel _corePanel;
    [SerializeField] private List<SkillPanel> _skillPanelList;
    [SerializeField] private StageData _stageData;
    [SerializeField] private TrackCamera _trackCamera;
    [SerializeField] private Vector3 _initialCameraPosition;
    private BattlePlayer _player;
  }
}

