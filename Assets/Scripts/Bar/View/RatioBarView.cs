using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class RatioBarView : BarView {
    public override void UpdateView(int cur, int max) {
      Assert.IsTrue(max > 0);

      double ratio = (double)cur / max * 100.0;
      double percent = Math.Round(ratio, 2);

      _text.text = cur + " [" + percent + "%]  ";
      Animate(cur, max);
    }

    [SerializeField] private Text _text;
  }
}

