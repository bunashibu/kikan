using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  public void PickManji() {
    _player = Instantiate(_player);
    Instantiate(_jobs[0]).transform.parent = _player.transform;
    DisableAllButtons();
  }

  public void PickMagician() {
    _player = Instantiate(_player);
    Instantiate(_jobs[1]).transform.parent = _player.transform;
    DisableAllButtons();
  }

  public void PickWarrior() {
    DisableAllButtons();
  }

  public void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  void Start() {
    Destroy(gameObject, 30.0f);
  }

  [SerializeField] GameObject _player;
  [SerializeField] Button[] _buttons;
  [SerializeField] GameObject[] _jobs;
}
