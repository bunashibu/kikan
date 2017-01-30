using UnityEngine;
using System.Collections;

public class WalkRightSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
    }

    Debug.Log("right");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _linearMove.MoveRight();

    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool LeftKeyDown      = Input.GetKeyDown(KeyCode.LeftArrow);
    bool RightKeyUp       = Input.GetKeyUp(KeyCode.RightArrow);

    if (OnlyLeftKeyDown)           { ActTransition("WalkLeft", animator);   return; }
    if (OnlyDownKeyDown && JumpButtonDown) { ActTransition("StepDownJump", animator); return; }
    if (JumpButtonDown)            { ActTransition("GroundJump", animator); return; }
    if (RightKeyUp || LeftKeyDown) { ActTransition("Idle", animator);       return; }
  }

  private void AirUpdate(Animator animator) {
    ActTransition("Fall", animator);
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("WalkRight", false);
  }

  private RigidState _rigidState;
  private GroundLinearMove _linearMove;
}

