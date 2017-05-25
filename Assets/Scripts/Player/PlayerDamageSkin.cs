using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSkin : MonoBehaviour {
  public DamageSkin Skin {
    get {
      return _skin;
    }
  }

  [SerializeField] private DamageSkin _skin;
}

