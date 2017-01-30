using UnityEngine;
using System.Collections;

public class GroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _groundLinearMove = animator.GetComponent<GroundLinearMove>();
      _airLinearMove = animator.GetComponent<AirLinearMove>();
      _jump = animator.GetComponent<GroundJump>();
    }

    Debug.Log("jump");
    _jump.Jump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);

    if (_rigidState.Air)
      AirUpdate();
  }

  private void GroundUpdate(Animator animator) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");

    if (JumpButtonDown) {
      _jump.Jump();

      if (OnlyLeftKeyDown)  _groundLinearMove.MoveLeft();
      if (OnlyRightKeyDown) _groundLinearMove.MoveRight();
      return;
    }

    if (OnlyLeftKeyDown)  { ActTransition("WalkLeft", animator);  return; }
    if (OnlyRightKeyDown) { ActTransition("WalkRight", animator); return; }
    ActTransition("Idle", animator);
  }

  private void AirUpdate() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  _airLinearMove.MoveLeft();
    if (OnlyRightKeyDown) _airLinearMove.MoveRight();
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("GroundJump", false);
  }

  private RigidState _rigidState;
  private GroundJump _jump;
  private GroundLinearMove _groundLinearMove;
  private AirLinearMove _airLinearMove;
}

