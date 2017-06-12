﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPanel : MonoBehaviour {
  public void UpdateGold(int cur) {
    _text.text = cur.ToString();
  }

  [SerializeField] private Text _text;
}
