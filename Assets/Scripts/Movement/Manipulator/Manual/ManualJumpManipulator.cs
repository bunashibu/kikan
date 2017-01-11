using UnityEngine;
using System.Collections;

public class ManualJumpManipulator : MonoBehaviour {
  void Update() {
    canJump = true; //!_isAir && !_isLying;

    if (canJump) {
      if (Input.GetButton("Jump"))
        _system.Jump();

      if (Input.GetButtonUp("Jump"))
        _system.Stay();
    }
  }

  [SerializeField] private JumpSystem _system;
  public bool canJump { get; private set; }
}

