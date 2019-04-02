using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class SettginsExit : MonoBehaviour {
    void Awake() {
      _button = GetComponent<Button>();
      _button.onClick.AddListener(() => CloseContent());

      _parent.SetActive(false);
    }

    private void CloseContent() {
      _parent.SetActive(false);
      SettingsStream.OnNextClose();
    }

    [SerializeField] private GameObject _parent;
    private Button _button;
  }
}

