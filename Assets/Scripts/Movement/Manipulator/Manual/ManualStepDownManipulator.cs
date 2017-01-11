using UnityEngine;
using System.Collections;

public class ManualStepDownManipulator : MonoBehaviour {
  void Update() {
    canStepDown = true; // isLying && On CanDownGround

    if (canStepDown) {
      if (Input.GetButton("Jump"))
        _system.StepDown();
    }
  }

  [SerializeField] private StepDownSystem _system;
  public bool canStepDown { get; private set; }
}

