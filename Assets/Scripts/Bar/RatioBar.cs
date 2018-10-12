using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public abstract class RatioBar : Bar {
    public void UpdateView(int cur, int max) {
      /*
      double ratio = (double)cur / max * 100.0;
      double percent = Math.Round(ratio, 2);

      _text.text = cur + " [" + percent + "%]  ";
      Animate(cur, max);
      */
    }

    public override void OnNotify(Notification notification, object[] args) {}

    [SerializeField] private Text _text;
  }

}

