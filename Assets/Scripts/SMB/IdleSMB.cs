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

    if (OnlyLeftKeyDown ) { animator.SetBool("WalkLeft"  , true); return; }
    if (OnlyRightKeyDown) { animator.SetBool("WalkRight" , true); return; }
    if (OnlyDownKeyDown ) { animator.SetBool("LieDown"   , true); return; }
    if (JumpButtonDown  ) { animator.SetBool("GroundJump", true); return; }
  }

  private void AirUpdate(Animator animator) {
    animator.SetBool("Fall", true);
  }

  private RigidState _rigidState;
}

