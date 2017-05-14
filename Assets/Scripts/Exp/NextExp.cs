using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level))]
public class NextExp : MonoBehaviour, IGauge<int> {
  public void Init() {
    _index = 0;
    Cur = 0;
    Min = 0;
    Max = _table.Data[_index];
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur >= Max)
      LvUp();
    if (Cur < Min)
      Cur = Min;
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  private void LvUp() {
    _level.LvUp();
    _index += 1;
    Max = _table.Data[_index];
    Cur = Min;
  }

  [SerializeField] private ExpTable _table;
  [SerializeField] private Level _level;
  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  private int _index;
}

