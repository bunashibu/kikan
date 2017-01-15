using UnityEngine;
using System.Collections;

public class LieDownManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetKey(KeyCode.DownArrow))
        _system.LieDown();

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        _system.Stay();
    }
  }

  [SerializeField] private LieDownSystem _system;
}

