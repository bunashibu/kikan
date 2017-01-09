using UnityEngine;
using System.Collections;

public class ManualLieDownManipulator : MonoBehaviour {
  void Update() {
    canLieDown = true;

    if (canLieDown) {
      if (Input.GetKey(KeyCode.DownArrow))
        _system.LieDown();

      if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        _system.Stay();
    }
  }

  [SerializeField] private LieDownSystem _system;
  public bool canLieDown { get; private set; }
}

