using UnityEngine;
using System.Collections;

public class Health : ScriptableObject, IGauge<int> {
  public void Init(int life, int maxLife) {
    Cur = life;
    Min = 0; // Until C#6
    Max = maxLife;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur < Min)
      Cur = Min;
    if (Cur > Max)
      Cur = Max;

    Dead = (Cur == Min) ? true : false;
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  public int Cur { get; private set; }
  public int Min { get; private set; } // = 0; C#6
  public int Max { get; private set; }
  public bool Dead { get; private set; }
}

