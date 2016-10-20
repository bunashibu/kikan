using UnityEngine;
using System.Collections;

public class Magician : Job {
  void Start() {
    GetComponent<SpriteRenderer>().sprite = _actionNormal;
  }
}
