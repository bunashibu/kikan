using UnityEngine;
using System.Collections;

public class ManualJumpManipulator : MonoBehaviour {
  void Update() {
    CanJump = true; //!_isAir && !_isLying;

    if (CanJump) {
      if (Input.GetButton("Jump"))
        _system.Jump();

      if (Input.GetButtonUp("Jump"))
        _system.Stay();
    }
  }

  [SerializeField] private JumpSystem _system;
  public bool CanJump { get; private set; }
}

