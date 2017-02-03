using UnityEngine;
using System.Collections;

public class FallSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _airLinearMove = animator.GetComponent<AirLinearMove>();
    }

    Debug.Log("Fall");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");

    if (_rigidState.Ladder) {
      if (OnlyUpKeyDown || OnlyDownKeyDown) { ActTransition("Climb", animator); return; }
    }

    if (_rigidState.Ground) {
      if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator);       return; }
      if (OnlyDownKeyDown)                     { ActTransition("LieDown", animator);    return; }
      if (JumpButtonDown)                      { ActTransition("GroundJump", animator); return; }
      ActTransition("Idle", animator); return;
    }

    if (_rigidState.Air) {
      if (OnlyLeftKeyDown)  _airLinearMove.MoveLeft();
      if (OnlyRightKeyDown) _airLinearMove.MoveRight();
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Fall", false);
  }

  private RigidState _rigidState;
  private AirLinearMove _airLinearMove;
}

