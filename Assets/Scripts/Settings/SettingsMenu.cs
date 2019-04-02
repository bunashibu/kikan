using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class SettingsMenu : MonoBehaviour {
    void Awake() {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(() => { Open(); });

      gameObject.SetActive(false);
    }

    private void Open() {
      var menu = Instantiate(_menuObj) as GameObject;
      menu.transform.SetParent(_canvas.transform, false);
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _menuObj;
    private Button _button;
  }
}

