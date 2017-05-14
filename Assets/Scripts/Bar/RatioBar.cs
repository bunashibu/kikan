using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatioBar : Bar {
  public override void Show(int cur, int max) {
    _text.text = cur + " [" + cur / max * 100.0 + "%]  ";
    Animate(cur, max);
  }

  [SerializeField] private Text _text;
}

