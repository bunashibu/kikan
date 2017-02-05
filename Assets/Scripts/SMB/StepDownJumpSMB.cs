using UnityEngine;
using System.Collections;

public class StepDownJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigidState = animator.GetComponent<RigidState>();
      _colliderFoot = animator.GetComponents<BoxCollider2D>()[1];
      _jump = animator.GetComponent<StepDownJump>();
    }

    Debug.Log("StepDown");

    InitFlag();
    _colliderFoot.isTrigger = true;
    _jump.StepDown();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (_rigidState.Air) {
      _isAlreadyJumped = true;
    }

    if (_rigidState.Ground && _isAlreadyJumped) {
      _fallFlag = true;
    }

    if (_rigidState.Air && _fallFlag) {
      ActTransition("Fall", animator); return;
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _colliderFoot.isTrigger = false;
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("StepDownJump", false);
  }

  private void InitFlag() {
    _isAlreadyJumped = false;
    _fallFlag = false;
  }

  private RigidState _rigidState;
  private StepDownJump _jump;
  private BoxCollider2D _colliderFoot;
  private bool _isAlreadyJumped;
  private bool _fallFlag;
}

