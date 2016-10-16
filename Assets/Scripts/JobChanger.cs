using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JobChanger : MonoBehaviour {
  public void PickManji() {
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

  [SerializeField] Button[] _buttons;
}
