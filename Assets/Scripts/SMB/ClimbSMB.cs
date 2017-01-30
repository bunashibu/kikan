using UnityEngine;
using System.Collections;

public class ClimbSMB : StateMachineBehaviour {
  /*
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _climb = animator.GetComponent<Climb>();
    }

    Debug.Log("climb");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _linearMove.MoveLeft();

    if (_rigidState.Ground)
      GroundUpdate(animator);

    else if (_rigidState.Air || _rigidState.Ladder)
      AirUpdate(animator);
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");
    bool LeftKeyUp        = Input.GetKeyUp(KeyCode.LeftArrow);
    bool RightKeyDown     = Input.GetKeyDown(KeyCode.RightArrow);

    if (OnlyRightKeyDown)          { ActTransition("WalkRight", animator);  return; }
    if (OnlyDownKeyDown && JumpButtonDown) { ActTransition("StepDownJump", animator); return; }
    if (JumpButtonDown)            { ActTransition("GroundJump", animator); return; }
    if (LeftKeyUp || RightKeyDown) { ActTransition("Idle", animator);       return; }
  }

  private void AirUpdate(Animator animator) {
    ActTransition("Fall", animator);
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("WalkLeft", false);
  }

  private RigidState _rigidState;
  private Climb _climb;
  */
}

