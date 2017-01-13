using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  void Start() {
    Destroy(gameObject, 10.0f);
  }

  public void Pick(int n) {
    _player = Instantiate(_player) as GameObject;

    var job = Instantiate(_jobs[n]) as GameObject;
    job.transform.SetParent(_player.transform, false);

    InitPlayerHealthSystem(n);
    InitPlayerStatus(n);
    InitPlayerMovementSystem(n);

    DisableAllButtons();
    Destroy(_camera);
  }

  private void InitPlayerHealthSystem(int n) {
    var health = ScriptableObject.CreateInstance<Health>();
    health.Init(_data[n].life, _data[n].life);

    _bar = Instantiate(_bar) as Bar;
    _bar.transform.SetParent(_canvas.transform, false);

    var hs = _player.GetComponent<HealthSystem>();
    hs.Init(health, _bar);
    hs.Show();
  }

  private void InitPlayerStatus(int n) {
    var status = _player.GetComponent<PlayerStatus>();

    status.atk = _data[n].atk;
    status.dfn = _data[n].dfn;
    status.spd = _data[n].spd;
    status.jmp = _data[n].jmp;
  }

  private void InitPlayerMovementSystem(int n) {
    var linearSystem = _player.GetComponent<LinearMoveSystem>();
    var jumpSystem = _player.GetComponent<JumpSystem>();

    linearSystem.SetForce(_data[n].spd);
    linearSystem.SetLimit(12);
    jumpSystem.SetForce(_data[n].jmp);
  }

  private void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _camera;
  [SerializeField] private Button[] _buttons;
  [SerializeField] private GameObject[] _jobs;
  [SerializeField] private JobStatus[] _data;
  [SerializeField] private Canvas _canvas;
  [SerializeField] private Bar _bar;
}

