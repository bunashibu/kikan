using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  public void PickManji() {
    PickImpl(0, 3000);
  }

  public void PickMagician() {
    PickImpl(1, 2200);
  }

  public void PickWarrior() {
    //PickImpl();
  }

  void PickImpl(int n, int life) {
    _player = Instantiate(_player);

    Job job = Instantiate(_jobs[n]).GetComponent<Job>();
    job.transform.parent = _player.transform;

    HealthSystem hs = _player.GetComponentInChildren<HealthSystem>();
    hs.Set(life, life);
    hpBar = Instantiate(hpBar);
    hpBar.transform.SetParent(manager.hud.transform, false);
    hs.bar = hpBar;
    hs.Show();

    DisableAllButtons();
    DeleteCamera();
  }

  void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  void DeleteCamera() {
    Destroy(_camera);
  }

  void Start() {
    Destroy(gameObject, 10.0f);
  }

  [SerializeField] GameObject _player;
  [SerializeField] GameObject _camera;
  [SerializeField] Button[] _buttons;
  [SerializeField] GameObject[] _jobs;
  [SerializeField] public BattleSceneManager manager;
  [SerializeField] public HealthBar hpBar;
}
