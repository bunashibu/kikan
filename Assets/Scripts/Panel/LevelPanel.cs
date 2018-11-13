using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class LevelPanel : MonoBehaviour {
    public void UpdateView(int curLevel) {
      _text.text = "Lv " + curLevel.ToString();
    }

    [SerializeField] private Text _text;
  }
}

