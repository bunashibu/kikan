using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillExp : MonoBehaviour {
  void Awake() {
    _index = 0;
  }

  public int Exp {
    get {
      return _table.Data[_index];
    }
  }

  [SerializeField] private ExpTable _table;
  [SerializeField] private Level _level;
  private int _index;
}

