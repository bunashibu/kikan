using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class KillMessage : MonoBehaviour {
    void Start() {
      Destroy(gameObject, 6.0f);
      _subtractColor = new Color(0, 0, 0, 0.0023f);
    }

    void Update() {
      _killText.color = _killText.color - _subtractColor;
      _messageText.color = _messageText.color - _subtractColor;
    }

    public void SetKillPlayerName(string killPlayerName, bool isSameTeam) {
      _killText.text = "[" + killPlayerName + "]";

      if (isSameTeam)
        _killText.color = Color.HSVToRGB(95.0f / 360.0f, 85.0f / 100.0f, 1.0f);
      else
        _killText.color = Color.HSVToRGB(304.0f / 360.0f, 70.0f / 100.0f, 1.0f);
    }

    public void SetDeathPlayerName(string deathPlayerName) {
      _messageText.text = "が[" + deathPlayerName + "]を制圧しました。";
    }

    [SerializeField] private Text _killText;
    [SerializeField] private Text _messageText;
    private Color _subtractColor;
  }
}

