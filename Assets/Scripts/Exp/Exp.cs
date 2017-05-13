using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour, IGauge<int> {
  public void Init() {
    Cur = 0;
    Min = 0;
    Max = _table.Data[_index];

    _index = 0;
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

  public void LvUp() {
    _status.lv += 1;
    _index += 1;

    Max = _table.Data[_index];
    Cur = Min;
  }

  [SerializeField] private ExpTable _table;
  [SerializeField] private PlayerStatus _status;
  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  private int _index;
}

