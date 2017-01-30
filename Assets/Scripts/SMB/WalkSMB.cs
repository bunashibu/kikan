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
    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool LeftKeyUp        = Input.GetKeyUp(KeyCode.LeftArrow);
    bool RightKeyDown     = Input.GetKeyDown(KeyCode.RightArrow);

    if (OnlyLeftKeyDown)  _linearMove.MoveLeft();
    if (OnlyRightKeyDown) _linearMove.MoveRight();

    if (OnlyDownKeyDown && JumpButtonDown) { ActTransition("StepDownJump", animator); return; }
    if (JumpButtonDown)            { ActTransition("GroundJump", animator); return; }
    if (LeftKeyUp || RightKeyDown) { ActTransition("Idle", animator);       return; }
  }

  private void AirUpdate(Animator animator) {
    ActTransition("Fall", animator);
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Walk", false);
  }

  private RigidState _rigidState;
  private GroundLinearMove _linearMove;
}

