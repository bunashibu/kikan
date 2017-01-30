using UnityEngine;
using System.Collections;

public class StepDownJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _airLinearMove = animator.GetComponent<AirLinearMove>();
      _colliderFoot = animator.GetComponents<BoxCollider2D>()[1];
      _jump = animator.GetComponent<StepDownJump>();
    }

    Debug.Log("StepDown");
    StepDown();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState.Ground && _transitionFlag)
      GroundUpdate();

    if (_rigidState.Air)
      AirUpdate(animator);
  }

  private void GroundUpdate() {
    _fallFlag = true;
  }

  private void AirUpdate(Animator animator) {
    _transitionFlag = true;

    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  _airLinearMove.MoveLeft();
    if (OnlyRightKeyDown) _airLinearMove.MoveRight();

    if (_fallFlag) {
      _colliderFoot.isTrigger = false;
      ActTransition("Fall", animator);
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("StepDownJump", false);
  }

  private void StepDown() {
    _transitionFlag = false;
    _fallFlag = false;
    _colliderFoot.isTrigger = true;
    _jump.StepDown();
  }

  private RigidState _rigidState;
  private StepDownJump _jump;
  private AirLinearMove _airLinearMove;
  private BoxCollider2D _colliderFoot;
  private bool _transitionFlag;
  private bool _fallFlag;
}

