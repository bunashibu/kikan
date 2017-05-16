using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KillDeathTeamPanel : MonoBehaviour {
  void Awake() {
    int size = _panels.Length;

    _nameText = new Text[size];
    _levelText = new Text[size];
    _killText = new Text[size];
    _deathText = new Text[size];

    _level = new int[size].Select(x => 1).ToArray();
    _kill = new int[size];
    _death = new int[size];

    for (int i=0; i<size; ++i) {
      Text[] texts = _panels[i].GetComponentsInChildren<Text>();
      _nameText[i] = texts[0];
      _levelText[i] = texts[1];
      _killText[i] = texts[2];
      _deathText[i] = texts[3];

      _nameText[i].text = "";
      UpdateText(ref _levelText[i], ref _level[i]);
      UpdateText(ref _killText[i], ref _kill[i]);
      UpdateText(ref _deathText[i], ref _death[i]);
    }
  }

  public void PlusLevel(int i) {
    _level[i] += 1;
    UpdateText(ref _levelText[i], ref _level[i]);
  }

  public void PlusKill(int i) {
    _kill[i] += 1;
    UpdateText(ref _killText[i], ref _kill[i]);
  }

  public void PlusDeath(int i) {
    _death[i] += 1;
    UpdateText(ref _deathText[i], ref _death[i]);
  }

  private void UpdateText(ref Text text, ref int n) {
    string indent = (n < 10) ? " " : "";
    text.text = indent + n;
  }

  [SerializeField] private GameObject[] _panels;
  private Text[] _nameText;
  private Text[] _levelText;
  private Text[] _killText;
  private Text[] _deathText;
  private int[] _level;
  private int[] _kill;
  private int[] _death;
}

