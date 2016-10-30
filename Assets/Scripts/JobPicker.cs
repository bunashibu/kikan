using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  public void PickManji() {
    _player = Instantiate(_player);

    Job job = Instantiate(_jobs[0]).GetComponent<Job>();
    job.transform.parent = _player.transform;

    hpBar = Instantiate(hpBar);
    hpBar.transform.SetParent(manager.hud.transform, false);
    hpBar.life = job.GetLife();
    hpBar.maxLife = job.GetMaxLife();

    DisableAllButtons();
    DeleteCamera();
  }

  public void PickMagician() {
    _player = Instantiate(_player);
    Instantiate(_jobs[1]).transform.parent = _player.transform;
    DisableAllButtons();
    DeleteCamera();
  }

  public void PickWarrior() {
    DisableAllButtons();
    DeleteCamera();
  }

  public void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  public void DeleteCamera() {
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
