using UnityEngine;
using System.Collections;

public class ManualStepDownJumpManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetButton("Jump"))
        _system.StepDown();
    }
  }

  [SerializeField] private StepDownJumpSystem _system;
}

