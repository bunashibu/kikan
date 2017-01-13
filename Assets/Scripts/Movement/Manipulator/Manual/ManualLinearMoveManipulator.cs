using UnityEngine;
using System.Collections;

public class ManualLinearMoveManipulator : MonoBehaviour {
  void Update() {
    CanMove = true; //!_isLadder;

    if (CanMove) {
      if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        _system.Stay();

      if (Input.GetKey(KeyCode.LeftArrow))
        _system.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _system.MoveRight();
    }
  }

  [SerializeField] private LinearMoveSystem _system;
  public bool CanMove { get; private set; }
}

