using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
  public void Init() {
    int initialLv = 0;
    _level.Init(initialLv);
  }

  public void LvUp() {
    _level.LvUp();
  }

  private Level _level;
}

