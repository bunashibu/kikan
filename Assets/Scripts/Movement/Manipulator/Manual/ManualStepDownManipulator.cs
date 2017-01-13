using UnityEngine;
using System.Collections;

public class ManualStepDownManipulator : MonoBehaviour {
  void Update() {
    CanStepDown = true; // isLying && On CanDownGround

    if (CanStepDown) {
      if (Input.GetButton("Jump"))
        _system.StepDown();
    }
  }

  [SerializeField] private StepDownSystem _system;
  public bool CanStepDown { get; private set; }
}

