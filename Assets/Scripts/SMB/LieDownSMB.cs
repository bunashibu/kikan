using UnityEngine;
using System.Collections;

public class LieDownSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();

    Debug.Log("liedown");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool DownKeyUp        = Input.GetKeyUp(KeyCode.DownArrow);
    bool UpKeyDown        = Input.GetKeyDown(KeyCode.UpArrow);

    if (_rigidState.Ground) {
      if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator);         return; }
      if (JumpButtonDown)                      { ActTransition("StepDownJump", animator); return; }
      if (DownKeyUp || UpKeyDown)              { ActTransition("Idle", animator);         return; }
    }

    if (_rigidState.Air) {
      ActTransition("Fall", animator); return;
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("LieDown", false);
  }

  private RigidState _rigidState;
}

