using UnityEngine;
using System.Collections;

public class Manji : Job {
  void Start() {
    GetComponent<SpriteRenderer>().sprite = _actionNormal;
  }
}
