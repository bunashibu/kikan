using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Bunashibu.Kikan {
  public class SettingsButton : MonoBehaviour {
    void Awake() {
      _button = GetComponent<Button>();

      _button.onClick.AddListener(() => {
        if (_isOpened) {
          CloseMenu();
          _audioSource.mute = false;
        }
        else {
          OpenMenu();
          _audioSource.mute = true;
        }
      });

      _audioSource = GetComponent<AudioSource>();

      SettingsStream.OnOpenContent
        .Subscribe(_ => {
          CloseMenu();
          Disable();
        })
        .AddTo(gameObject);

      SettingsStream.OnCloseContent
        .Subscribe(_ => Enable() )
        .AddTo(gameObject);
    }

    void Start() {
      CloseMenu();
    }

    public void OpenMenu() {
      _isOpened = true;

      foreach (var menuButton in _menuButtons)
        menuButton.Display();
    }

    public void CloseMenu() {
      _isOpened = false;

      foreach (var menuButton in _menuButtons)
        menuButton.Hide();
    }

    public void Enable() {
      _button.interactable = true;
      _audioSource.mute = false;
    }

    public void Disable() {
      _button.interactable = false;
      _audioSource.mute = true;
    }

    [SerializeField] private List<SettingsMenuButton> _menuButtons;
    private AudioSource _audioSource;
    private Button _button;
    private bool _isOpened;
  }
}

