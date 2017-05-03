using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    // Team 0 is Red(Right), Team 1 is Blue(Left)
    float x = 37.0f;
    float y = -2.5f;
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
      x *= -1;

    _player = PhotonNetwork.Instantiate("Prehabs/Job/" + _jobs[n].name, new Vector3(x, y, 0), Quaternion.identity, 0);

    var renderers = _player.GetComponentsInChildren<SpriteRenderer>();
    if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1) {
      foreach (var sprite in renderers)
        sprite.flipX = true;
    }

    InitPlayerHealth(n);
    InitPlayerStatus(n);
    InitPlayerMovement(n);

    DisableAllButtons();
    Destroy(_camera);
  }

  private void InitPlayerHealth(int n) {
    _bar = Instantiate(_bar) as Bar;
    _bar.transform.SetParent(_canvas.transform, false);

    var playerHealth = _player.GetComponent<PlayerHealth>();
    playerHealth.Init(_data[n].life, _bar);

    _player.GetComponent<PhotonView>().RPC("Show", PhotonTargets.All);
  }

  private void InitPlayerStatus(int n) {
    var status = _player.GetComponent<PlayerStatus>();

    status.lv  = 1;
    status.atk = _data[n].atk;
    status.dfn = _data[n].dfn;
    status.spd = _data[n].spd;
    status.jmp = _data[n].jmp;
  }

  private void InitPlayerMovement(int n) {
    var linearMove = _player.GetComponent<GroundLinearMove>();
    var jump = _player.GetComponent<GroundJump>();

    linearMove.SetForce(_data[n].spd);
    jump.SetForce(_data[n].jmp);
  }

  private void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  [SerializeField] private GameObject[] _jobs;
  [SerializeField] private GameObject _camera;
  [SerializeField] private Button[] _buttons;
  [SerializeField] private JobStatus[] _data;
  [SerializeField] private Canvas _canvas;
  [SerializeField] private Bar _bar;
  private GameObject _player;
}

