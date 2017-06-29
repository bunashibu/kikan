using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class JobPicker : MonoBehaviour {
  void Start() {
    InstantiateHudObjects();
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    InstantiatePlayer(n);

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
  }

  private void InstantiateHudObjects() {
    _hpBar = Instantiate(_hpBar) as Bar;
    _hpBar.transform.SetParent(_canvas.transform, false);

    _expBar = Instantiate(_expBar) as Bar;
    _expBar.transform.SetParent(_canvas.transform, false);

    _lvPanel = Instantiate(_lvPanel) as LevelPanel;
    _lvPanel.transform.SetParent(_canvas.transform, false);
  }

  private void InstantiatePlayer(int n) {
    var pos = _gameData.RespawnPosition;
    // INFO: Team 0 is Red(Right), Team 1 is Blue(Left)
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
      pos.x *= -1;

    _player = PhotonNetwork.Instantiate("Prehabs/Job/" + _jobs[n].name, pos, Quaternion.identity, 0).GetComponent<BattlePlayer>();
    AdjustFlipX();
    SetViewID();
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

  private void InitPlayerHp() {
    _player.Hp.AttachHudBar(_hpBar);
    _player.SyncObserver.SyncUpdateHpView();
  }

  private void InitPlayerExp() {
    /*
    var playerNextExp = _player.NextExp;
    playerNextExp.Init(_expBar);
    playerNextExp.UpdateView();
    */
  }

  private void InitPlayerLv() {
    /*
    var playerLv = _player.GetComponent<PlayerLevel>();
    playerLv.Init(_lvPanel, _kdPanel);
    playerLv.UpdateView();
    */
  }

  private void InitPlayerKillDeath() {
    /*
    var playerKDRec = _player.GetComponent<PlayerKillDeath>();
    playerKDRec.Init(_kdPanel);
    */
  }

  private void InitPlayerGold() {
    /*
    var playerGold = _player.GetComponent<PlayerGold>();
    playerGold.Init(_goldPanel);
    */
  }

  private void InitPlayerCore() {
    /*
    var playerCore = _player.GetComponent<PlayerCore>();
    playerCore.Init(_corePanel);
    */
  }

  private void InitPlayerStatus(int n) {
    /*
    var status = _player.GetComponent<PlayerStatus>();
    status.Init(_jobData[n]);
    */
  }

  private void InitPlayerMovement(int n) {
    /*
    var linearMove = _player.GetComponent<GroundLinearMove>();
    var jump = _player.GetComponent<GroundJump>();

    linearMove.SetForce(_jobData[n].Spd);
    jump.SetForce(_jobData[n].Jmp);
    */
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
  [SerializeField] private GameData _gameData;
  private BattlePlayer _player;
}

