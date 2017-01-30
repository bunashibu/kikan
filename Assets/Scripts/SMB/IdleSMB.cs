using UnityEngine;
using System.Collections;

public class IdleSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null)
      _rigidState = animator.GetComponent<RigidState>();

    Debug.Log("idle");
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

    if (OnlyLeftKeyDown)  { ActTransition("WalkLeft", animator);   return; }
    if (OnlyRightKeyDown) { ActTransition("WalkRight", animator);  return; }
    if (OnlyDownKeyDown)  { ActTransition("LieDown", animator);    return; }
    if (JumpButtonDown)   { ActTransition("GroundJump", animator); return; }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
    animator.SetBool("Idle", false);
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Idle", false);
  }

  private RigidState _rigidState;
}

