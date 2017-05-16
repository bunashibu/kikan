using UnityEngine;
using System.Collections;

public class ClimbJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _renderers = animator.GetComponentsInChildren<SpriteRenderer>();
      _jump = animator.GetComponent<ClimbJump>();
      _linearMove = animator.GetComponent<GroundLinearMove>();
      _hp = animator.GetComponent<PlayerHp>();
    }

    Debug.Log("ClimbJump");

    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

    if (OnlyLeftKeyDown)  { _linearMove.MoveLeft(); foreach (var sprite in _renderers) sprite.flipX = false; }
    if (OnlyRightKeyDown) { _linearMove.MoveRight(); foreach (var sprite in _renderers) sprite.flipX = true; }

    _jump.JumpOff();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      if (_hp.Dead) { ActTransition("Die", animator); return; }
      if (_rigidState.Air) { ActTransition("Fall", animator); return; }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("ClimbJump", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private SpriteRenderer[] _renderers;
  private ClimbJump _jump;
  private GroundLinearMove _linearMove;
  private PlayerHp _hp;
}

