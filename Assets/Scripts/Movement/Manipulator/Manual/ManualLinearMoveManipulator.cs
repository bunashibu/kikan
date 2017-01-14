using UnityEngine;
using System.Collections;

public class ManualLinearMoveManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetKey(KeyCode.LeftArrow))
        _system.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _system.MoveRight();
    }
  }

  [SerializeField] private LinearMoveSystem _system;
}

