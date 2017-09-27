using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class LevelPanel : MonoBehaviour {
    public void UpdateView(int lv) {
      _text.text = "Lv " + lv;
    }
  
    [SerializeField] private Text _text;
  }
}

