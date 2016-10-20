using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobPicker : MonoBehaviour {
  public void PickManji() {
    _battlePlayer = Instantiate(_battlePlayer);
    Instantiate(_jobs[0]).transform.parent = _battlePlayer.transform;
    DisableAllButtons();
  }

  public void PickMagician() {
    _battlePlayer = Instantiate(_battlePlayer);
    Instantiate(_jobs[1]).transform.parent = _battlePlayer.transform;
    DisableAllButtons();
  }

  public void PickWarrior() {
    DisableAllButtons();
  }

  public void DisableAllButtons() {
    foreach (Button button in _buttons)
      button.interactable = false;
  }

  public void HideAllButtons() {
    gameObject.SetActive(false);
  }

  [SerializeField] GameObject _battlePlayer;
  [SerializeField] Button[] _buttons;
  [SerializeField] GameObject[] _jobs;
}
