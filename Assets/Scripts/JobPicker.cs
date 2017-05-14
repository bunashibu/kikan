using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    // Team 0 is Red(Right), Team 1 is Blue(Left)
    var pos = _gameData.RespawnPosition;
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
      pos.x *= -1;

    _player = PhotonNetwork.Instantiate("Prehabs/Job/" + _jobs[n].name, pos, Quaternion.identity, 0);

    var renderers = _player.GetComponentsInChildren<SpriteRenderer>();
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1) {
      foreach (var sprite in renderers)
        sprite.flipX = true;
    }

    InitPlayerHealth(n);
    InitPlayerExp();
    InitPlayerStatus(n);
    InitPlayerMovement(n);

    DisableAllButtons();
    Destroy(_camera);
  }

  private void InitPlayerHealth(int n) {
    _hudHpBar = Instantiate(_hudHpBar) as Bar;
    _hudHpBar.transform.SetParent(_canvas.transform, false);

    var playerHealth = _player.GetComponent<PlayerHealth>();
    playerHealth.Init(_jobData[n].Life, _hudHpBar);
    playerHealth.Show();
  }

  private void InitPlayerExp() {
    _hudExpBar = Instantiate(_hudExpBar) as Bar;
    _hudExpBar.transform.SetParent(_canvas.transform, false);

    var playerNextExp = _player.GetComponent<PlayerNextExp>();
    playerNextExp.Init(_hudExpBar);
    playerNextExp.Show();
  }

  private void InitPlayerStatus(int n) {
    var status = _player.GetComponent<PlayerStatus>();

    status.Atk = _jobData[n].Atk;
    status.Dfn = _jobData[n].Dfn;
    status.Spd = _jobData[n].Spd;
    status.Jmp = _jobData[n].Jmp;
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
  [SerializeField] private GameData _gameData;
  private GameObject _player;
}

