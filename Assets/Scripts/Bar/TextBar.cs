using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bunashibu.Kikan {
  public class TextBar : Bar {
    public override void UpdateView(int cur, int max) {
      _text.text = "[" + cur.ToString() + "/" + max.ToString() + "]  ";
      Animate(cur, max);
    }

    [SerializeField] private Text _text;
  }
}

