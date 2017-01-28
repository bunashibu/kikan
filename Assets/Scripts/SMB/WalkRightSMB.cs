using UnityEngine;
using System.Collections;

public class WalkRightSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
    }
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _linearMove.MoveRight();

    if (_rigidState.Ground) {
      GroundUpdate(animator);
      return;
    }

    if (_rigidState.Air) {
      AirUpdate(animator);
      return;
    }
  }

  private void GroundUpdate(Animator animator) {
    if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
      animator.SetBool("WalkLeft", true);
      animator.SetBool("WalkRight", false);
      return;
    }

    if (Input.GetButton("Jump")) {
      animator.SetBool("GroundJump", true);
      animator.SetBool("WalkRight", false);
      return;
    }

    if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) {
      animator.SetBool("Idle", true);
      animator.SetBool("WalkRight", false);
      return;
    }

    if (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
      animator.SetBool("Idle", true);
      animator.SetBool("WalkRight", false);
      return;
    }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
    animator.SetBool("Idle", false);
  }

  private RigidState _rigidState;
  private GroundLinearMove _linearMove;
}

