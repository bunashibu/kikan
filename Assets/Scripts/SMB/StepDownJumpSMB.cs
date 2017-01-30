using UnityEngine;
using System.Collections;

public class StepDownJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _colliderFoot = animator.GetComponent<BoxCollider2D>();
      _airLinearMove = animator.GetComponent<AirLinearMove>();
      _jump = animator.GetComponent<StepDownJump>();
    }

    Debug.Log("StepDown");
    _jump.StepDown(_colliderFoot);
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground)
      GroundUpdate(animator);

    if (_rigidState.Air)
      AirUpdate();
  }

  private void GroundUpdate(Animator animator) {
    _colliderFoot.isTrigger = false;
    ActTransition("Fall", animator);
  }

  private void AirUpdate() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  _airLinearMove.MoveLeft();
    if (OnlyRightKeyDown) _airLinearMove.MoveRight();
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("StepDownJump", false);
  }

  private RigidState _rigidState;
  private BoxCollider2D _colliderFoot;
  private StepDownJump _jump;
  private AirLinearMove _airLinearMove;
}

