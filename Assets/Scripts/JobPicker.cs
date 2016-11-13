using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  public void PickManji() {
    PickImpl(0, 30);
  }

  public void PickMagician() {
    PickImpl(1, 2200);
  }

  public void PickWarrior() {
    //PickImpl();
  }

  void PickImpl(int n, int life) {
    _player = Instantiate(_player) as GameObject;

    Instantiate(_jobs[n]).transform.SetParent(_player.transform, false);

    HealthSystem hs = _player.GetComponent<HealthSystem>();
    hs.Init(life, life, manager);
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

  [SerializeField] private GameObject _player;
  [SerializeField] private GameObject _camera;
  [SerializeField] private Button[] _buttons;
  [SerializeField] private GameObject[] _jobs;
  [SerializeField] private BattleSceneManager manager;
}
