using UnityEngine;
using System.Collections;

public class ClimbSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_rigidState == null) {
      _rigid = animator.GetComponent<Rigidbody2D>();
      _colliderFoot = animator.GetComponents<BoxCollider2D>()[1];
      _rigidState = animator.GetComponent<RigidState>();
      _climb = animator.GetComponent<Climb>();
    }

    Debug.Log("climb");

    _rigid.isKinematic = true;
    _colliderFoot.isTrigger = true;
    _isTransferable = false;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
    bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
    bool JumpButtonDown   = Input.GetButton("Jump");

    if (OnlyUpKeyDown)   _climb.MoveUp();
    if (OnlyDownKeyDown) _climb.MoveDown();

    if (_rigidState.Ladder)
      _isTransferable = true;

    if (_isTransferable) {
      if ((OnlyLeftKeyDown || OnlyRightKeyDown) && JumpButtonDown) {
        ActTransition("ClimbJump", animator); return;
      }

      // XXX : If rigid velocity is too fast, passing through ground will occur.
      if (_rigidState.LadderBottomEdge && _rigidState.Ground) {
        ActTransition("Idle", animator); return;
      }

      if (_rigidState.Air && !_rigidState.Ladder) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    _rigid.isKinematic = false;
    _colliderFoot.isTrigger = false;
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Climb", false);
  }

  private Rigidbody2D _rigid;
  private BoxCollider2D _colliderFoot;
  private RigidState _rigidState;
  private Climb _climb;
  private bool _isTransferable;
}

