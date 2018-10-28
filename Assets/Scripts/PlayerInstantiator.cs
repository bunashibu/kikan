using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class PlayerInstantiator : MonoBehaviour {
    void Start() {
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        _initialCameraPosition.x *= -1;

      _trackCamera.SetPosition(_initialCameraPosition);
    }

    public void InstantiatePlayer(string jobName) {
      var pos = StageReference.Instance.StageData.RespawnPosition;

      // NOTE: Team 0 is Red(Right), Team 1 is Blue(Left)
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        pos.x *= -1;

      _player = PhotonNetwork.Instantiate("Prefabs/Job/" + jobName, pos, Quaternion.identity, 0).GetComponent<BattlePlayer>();
      _player.Observer.SyncTeam();
    }

    public void InstantiateHudObjects(Canvas canvas, SkillPanel skillPanel) {
      _hpBar = Instantiate(_hpBar) as Bar;
      _hpBar.transform.SetParent(canvas.transform, false);

      _expBar = Instantiate(_expBar) as Bar;
      _expBar.transform.SetParent(canvas.transform, false);

      _lvPanel = Instantiate(_lvPanel) as LevelPanel;
      _lvPanel.transform.SetParent(canvas.transform, false);

      skillPanel = Instantiate(skillPanel) as SkillPanel;
      skillPanel.transform.SetParent(canvas.transform, false);
      _player.Weapon.SkillInstantiator.AttachSkillPanel(skillPanel);
    }

    public void InitAll(JobStatus jobStatus) {
      SetViewID();

      InitPlayerTeam();
      _player.Hp.AddListener(_hpBar.OnNotify);
      _player.Exp.AddListener(_expBar.OnNotify);
      _player.Level.AddListener(_lvPanel.OnNotify);
      //_player.Level.AddListener(_kdPanel.OnNotify);
      _player.Gold.AddListener(_goldPanel.OnNotify);
      InitPlayerKillDeath();
      InitPlayerCore();
      InitPlayerStatus(jobStatus);
      InitPlayerMovement(jobStatus);

      var notifier = new Notifier(_player.OnNotify);
      notifier.Notify(Notification.PlayerInstantiated);

      _trackCamera.SetTrackTarget(_player.gameObject);
    }

    private void SetViewID() {
      var viewID = _player.PhotonView.viewID;

      var props = new Hashtable() {{"ViewID", viewID}};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void InitPlayerTeam() {
      int team = (int)PhotonNetwork.player.CustomProperties["Team"];

      if (team == 0)
        _player.NameBackground.SetColor("RED");
      else if (team == 1)
        _player.NameBackground.SetColor("BLUE");

      _player.Observer.SyncNameBackground();

      foreach (var sprite in _player.Renderers) {
        if (team == 0)
          sprite.flipX = false;
        else if (team == 1)
          sprite.flipX = true;
      }
    }

    private void InitPlayerKillDeath() {
      var playerKDRec = _player.KillDeath;
      playerKDRec.Init(_kdPanel);
    }

    private void InitPlayerCore() {
      var playerCore = _player.Core;
      playerCore.Init(_corePanel);
    }

    private void InitPlayerStatus(JobStatus jobStatus) {
      var status = _player.Status;
      status.Init(jobStatus);
    }

    private void InitPlayerMovement(JobStatus jobStatus) {
      _player.Movement.SetMoveForce(jobStatus.Spd);
      _player.Movement.SetJumpForce(jobStatus.Jmp);
    }

    [SerializeField] private Bar _hpBar;
    [SerializeField] private Bar _expBar;
    [SerializeField] private LevelPanel _lvPanel;
    [SerializeField] private KillDeathPanel _kdPanel;
    [SerializeField] private CorePanel _corePanel;
    [SerializeField] private GoldPanel _goldPanel;
    [SerializeField] private TrackCamera _trackCamera;
    [SerializeField] private Vector3 _initialCameraPosition;
    private BattlePlayer _player;
  }
}

