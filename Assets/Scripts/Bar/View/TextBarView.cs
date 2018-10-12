using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class TextBarView : BarView {
    public override void UpdateView(int cur, int max) {
      _text.text = "[" + cur.ToString() + "/" + max.ToString() + "]  ";
      Animate(cur, max);
    }

    [SerializeField] private Text _text;
  }
}

