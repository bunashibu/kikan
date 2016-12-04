using UnityEngine;
using System.Collections;

public class Health : ScriptableObject, ISlider<int> {
  public void Init(int life, int maxLife) {
    Cur = life;
    Max = maxLife;
  }

  public int Cur { get; private set; }
  public int Max { get; private set; }
  public bool Dead { get; private set; }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur < 0)
      Cur = 0;
    if (Cur > Max)
      Cur = Max;

    Dead = (Cur == 0) ? true : false;
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }
}

