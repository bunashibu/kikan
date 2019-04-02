using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class SettingsPanel : MonoBehaviour {
    void Awake() {
      _button = GetComponent<Button>();

      _button.onClick.AddListener(() => {
        if (_isOpened)
          CloseMenu();
        else
          OpenMenu();
      });
    }

    private void OpenMenu() {
      _isOpened = true;

      foreach (var menu in _menus)
        menu.SetActive(true);

    }

    private void CloseMenu() {
      _isOpened = false;

      foreach (var menu in _menus)
        menu.SetActive(false);

    }

    [SerializeField] private List<GameObject> _menus;
    private Button _button;
    private bool _isOpened;
  }
}

