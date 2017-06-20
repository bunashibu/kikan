using UnityEngine;
using System.Collections;

public class StepDownJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView   = animator.GetComponent<PhotonView>();
      _rigidState   = animator.GetComponent<RigidState>();
      _colliderFoot = animator.GetComponents<BoxCollider2D>()[1];

      _movement     = animator.GetComponent<LobbyPlayer>().Movement;
      _hp           = animator.GetComponent<PlayerHp>();
    }

    Debug.Log("StepDown");

    InitFlag();
    _colliderFoot.isTrigger = true;
    _movement.StepDownJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      if (_hp.Dead) { ActTransition("Die", animator); return; }

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

  private PhotonView _photonView;
  private RigidState _rigidState;
  private BoxCollider2D _colliderFoot;

  private LobbyPlayerMovement _movement;
  private PlayerHp _hp;
  private bool _isAlreadyJumped;
  private bool _fallFlag;
}

