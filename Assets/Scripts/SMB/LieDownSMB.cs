using UnityEngine;
using System.Collections;

public class LieDownSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();

    Debug.Log("liedown");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool DownKeyUp        = Input.GetKeyUp(KeyCode.DownArrow);
    bool UpKeyDown        = Input.GetKeyDown(KeyCode.UpArrow);

    if (OnlyLeftKeyDown)        { ActTransition("WalkLeft", animator);     return; }
    if (OnlyRightKeyDown)       { ActTransition("WalkRight", animator);    return; }
    if (JumpButtonDown)         { ActTransition("StepDownJump", animator); return; }
    if (DownKeyUp || UpKeyDown) { ActTransition("Idle", animator);         return; }
  }

  private void AirUpdate(Animator animator) {
    ActTransition("Fall", animator);
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("LieDown", false);
  }

  private RigidState _rigidState;
}

