using UnityEngine;
using System.Collections;

public class ManualJumpManipulator : MonoBehaviour {
  void Update() {
    // canJump = !_isAir && !_isLying;

    if (canJump) {
      if (Input.GetButtonUp("Jump"))
        _system.Stay();

      if (Input.GetButton("Jump"))
        _system.Jump();
    }
  }

  [SerializeField] private JumpSystem _system;
  public bool canJump { get; private set; }
}

