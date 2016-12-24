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

    DisableAllButtons();
    Destroy(_camera);
  }

  private void InitPlayerHealthSystem(int n) {
    var health = ScriptableObject.CreateInstance<Health>();
    health.Init(_data[n].life, _data[n].life);

    _hBar = Instantiate(_hBar) as HealthBar;
    _hBar.transform.SetParent(_canvas.transform, false);

    var hs = _player.GetComponent<HealthSystem>();
    hs.Init(health, _hBar);
    hs.Show();
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
  [SerializeField] private HealthBar _hBar;
}

