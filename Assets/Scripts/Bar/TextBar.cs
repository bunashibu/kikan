using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bunashibu.Kikan {
  public abstract class TextBar : Bar {
    public void UpdateView(int cur, int max) {
      /*
      _text.text = "[" + cur.ToString() + "/" + max.ToString() + "]  ";
      Animate(cur, max);
      */
    }

    public override void OnNotify(Notification notification, object[] args) {}

    [SerializeField] private Text _text;
  }
}

