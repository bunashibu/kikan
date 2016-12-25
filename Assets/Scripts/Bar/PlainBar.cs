using UnityEngine;
using System.Collections;

public class PlainBar : Bar {
  public override void Show(int cur, int max) {
    Animate(cur, max);
  }
}

