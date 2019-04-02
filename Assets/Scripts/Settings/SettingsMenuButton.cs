using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Bunashibu.Kikan {
  public class SettingsMenuButton : MonoBehaviour {
    void Awake() {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(() => { OpenContent(); });
    }

    public void Display() {
      gameObject.SetActive(true);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    private void OpenContent() {
      _menuContentObj.SetActive(true);
      SettingsStream.OnNextOpen();
    }

    [SerializeField] private GameObject _menuContentObj;
    private Button _button;
  }
}

