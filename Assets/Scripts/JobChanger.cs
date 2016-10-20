using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobChanger : MonoBehaviour {
  public void PickManji() {
    Instantiate(_jobs[0]).transform.parent = _battlePlayer.transform;
    DisableAllButtons();
  }

  public void PickMagician() {
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
