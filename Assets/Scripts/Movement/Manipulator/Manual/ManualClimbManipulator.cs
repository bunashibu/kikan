using UnityEngine;
using System.Collections;

public class ManualClimbManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetKey(KeyCode.UpArrow))
        _system.MoveUp();

      if (Input.GetKey(KeyCode.DownArrow))
        _system.MoveDown();
    }
  }

  [SerializeField] private ClimbSystem _system;
}

