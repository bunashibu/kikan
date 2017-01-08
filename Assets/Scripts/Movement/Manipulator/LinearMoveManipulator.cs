using UnityEngine;
using System.Collections;

public class LinearMoveManipulator : MonoBehaviour {
  void Update() {
    //canMove = !_isLadder && !_isLying;

    if (canMove) {
      if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        _moveSys.Stay();

      if (Input.GetKey(KeyCode.LeftArrow))
        _moveSys.MoveLeft();

      if (Input.GetKey(KeyCode.RightArrow))
        _moveSys.MoveRight();
    }
  }

  [SerializeField] private LinearMoveSystem _moveSys;
  public bool canMove { get; private set; }
}

