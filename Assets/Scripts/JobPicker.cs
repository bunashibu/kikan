using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    InstantiatePlayer(n);

    InitPlayerHp(n);
    InitPlayerExp();
    InitPlayerLv();
    InitPlayerKillDeath();
    InitPlayerGold();
    InitPlayerStatus(n);
    InitPlayerMovement(n);

    DisableAllButtons();
    Destroy(_camera);
  }

  private void InstantiatePlayer(int n) {
    // Team 0 is Red(Right), Team 1 is Blue(Left)
    var pos = _gameData.RespawnPosition;
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
      pos.x *= -1;

    _player = PhotonNetwork.Instantiate("Prehabs/Job/" + _jobs[n].name, pos, Quaternion.identity, 0);
    AdjustFlipX();
    SetViewID();
  }

  private void AdjustFlipX() {
    var renderers = _player.GetComponentsInChildren<SpriteRenderer>();
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1) {
      foreach (var sprite in renderers)
        sprite.flipX = true;
    }
  }

  private void SetViewID() {
    var viewID = _player.GetComponent<PhotonView>().viewID;

    var props = new Hashtable() {{"ViewID", viewID}};
    PhotonNetwork.player.SetCustomProperties(props);
  }

  private void InitPlayerHp(int n) {
    _hudHpBar = Instantiate(_hudHpBar) as Bar;
    _hudHpBar.transform.SetParent(_canvas.transform, false);

    var playerHp = _player.GetComponent<PlayerHp>();
    playerHp.Init(_jobData[n].Life, _hudHpBar);
    playerHp.UpdateView();
  }

  private void InitPlayerExp() {
    _hudExpBar = Instantiate(_hudExpBar) as Bar;
    _hudExpBar.transform.SetParent(_canvas.transform, false);

    var playerNextExp = _player.GetComponent<PlayerNextExp>();
    playerNextExp.Init(_hudExpBar);
    playerNextExp.UpdateView();
  }

  private void InitPlayerLv() {
    _lvPanel = Instantiate(_lvPanel) as LevelPanel;
    _lvPanel.transform.SetParent(_canvas.transform, false);

    var playerLv = _player.GetComponent<PlayerLevel>();
    playerLv.Init(_lvPanel, _kdPanel);
    playerLv.UpdateView();
  }

  private void InitPlayerKillDeath() {
    var playerKDRec = _player.GetComponent<PlayerKillDeath>();
    playerKDRec.Init(_kdPanel);
  }

  private void InitPlayerGold() {
    var playerGold = _player.GetComponent<PlayerGold>();
    playerGold.Init(_goldPanel);
  }

  private void InitPlayerStatus(int n) {
    var status = _player.GetComponent<PlayerStatus>();
    status.Init(_jobData[n]);
  }

  private void InitPlayerMovement(int n) {
    var linearMove = _player.GetComponent<GroundLinearMove>();
    var jump = _player.GetComponent<GroundJump>();

    linearMove.SetForce(_jobData[n].Spd);
    jump.SetForce(_jobData[n].Jmp);
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
  [SerializeField] private Bar _hudHpBar;
  [SerializeField] private Bar _hudExpBar;
  [SerializeField] private LevelPanel _lvPanel;
  [SerializeField] private KillDeathPanel _kdPanel;
  [SerializeField] private GoldPanel _goldPanel;
  [SerializeField] private GameData _gameData;
  private GameObject _player;
}

