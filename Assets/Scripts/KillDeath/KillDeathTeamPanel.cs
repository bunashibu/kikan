using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class KillDeathTeamPanel : MonoBehaviour {
    void Awake() {
      int size = _panels.Length;

      _nameText = new Text[size];
      _lvText = new Text[size];
      _killText = new Text[size];
      _deathText = new Text[size];

      for (int i=0; i<size; ++i) {
        Text[] texts = _panels[i].GetComponentsInChildren<Text>();
        _nameText[i] = texts[0];
        _lvText[i] = texts[1];
        _killText[i] = texts[2];
        _deathText[i] = texts[3];

        _nameText[i].text = "";
        _lvText[i].text = " 1";
        _killText[i].text = " 0";
        _deathText[i].text = " 0";
      }
    }

    public void UpdateNameView(string name, int i) {
      _nameText[i].text = name;
    }

    public void UpdateLvView(int lv, int i) {
      UpdateText(ref _lvText[i], lv);
    }

    public void UpdateKillView(int kill, int i) {
      UpdateText(ref _killText[i], kill);
    }

    public void UpdateDeathView(int death, int i) {
      UpdateText(ref _deathText[i], death);
    }

    private void UpdateText(ref Text text, int n) {
      string indent = (n < 10) ? " " : "";
      text.text = indent + n;
    }

    [SerializeField] private GameObject[] _panels;
    private Text[] _nameText;
    private Text[] _lvText;
    private Text[] _killText;
    private Text[] _deathText;
  }
}

