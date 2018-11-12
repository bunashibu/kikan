using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class GoldPanel : MonoBehaviour {
    public void UpdateView(int curGold) {
      _text.text = curGold.ToString();
    }

    [SerializeField] private Text _text;
  }
}

