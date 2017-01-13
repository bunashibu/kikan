using UnityEngine;
using System.Collections;

public class ManualStepDownManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetButton("Jump"))
        _system.StepDown();
    }
  }

  [SerializeField] private StepDownSystem _system;
}

