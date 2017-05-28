using UnityEngine;
using System.Collections;

public class PlainBar : Bar {
  public override void UpdateView(int cur, int max) {
    Animate(cur, max);
  }
}

