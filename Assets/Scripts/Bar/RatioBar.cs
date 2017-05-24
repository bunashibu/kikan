using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatioBar : Bar {
  public override void Show(int cur, int max) {
    double ratio = (double)cur / max * 100.0;
    double percent = Math.Round(ratio, 2);

    _text.text = cur + " [" + percent + "%]  ";
    Animate(cur, max);
  }

  [SerializeField] private Text _text;
}

