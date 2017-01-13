using UnityEngine;
using System.Collections;

public class ManualJumpManipulator : MonoBehaviour {
  void Update() {
    if (true) {
      if (Input.GetButton("Jump"))
        _system.Jump();
    }
  }

  [SerializeField] private JumpSystem _system;
}

