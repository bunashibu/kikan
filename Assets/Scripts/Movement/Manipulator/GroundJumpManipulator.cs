using UnityEngine;
using System.Collections;

public class GroundJumpManipulator : MonoBehaviour {
  void Update() {
    if (_system.CanUse) {
      if (Input.GetButton("Jump"))
        _system.Jump();
    }
  }

  [SerializeField] private GroundJumpSystem _system;
}

