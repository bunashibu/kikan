using UnityEngine;
using System.Collections;

public class WalkSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
    }

    Debug.Log("walk");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool BothKeyDown      = Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow);
    bool OneKeyUp         = Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);

    if (_rigidState.Ladder) {
      if (OnlyUpKeyDown || OnlyDownKeyDown) { ActTransition("Climb", animator); return; }
    }

    if (_rigidState.Ground) {
      if (OnlyLeftKeyDown)  _linearMove.MoveLeft();
      if (OnlyRightKeyDown) _linearMove.MoveRight();

      if (OnlyDownKeyDown && JumpButtonDown) { ActTransition("StepDownJump", animator); return; }
      if (JumpButtonDown)          { ActTransition("GroundJump", animator); return; }
      if (BothKeyDown || OneKeyUp) { ActTransition("Idle", animator);       return; }
    }

    if (_rigidState.Air) {
      ActTransition("Fall", animator); return;
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Walk", false);
  }

  private RigidState _rigidState;
  private GroundLinearMove _linearMove;
}

