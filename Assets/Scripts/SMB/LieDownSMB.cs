using UnityEngine;
using System.Collections;

public class LieDownSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _hp = animator.GetComponent<PlayerHp>();
    }

    Debug.Log("liedown");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool JumpButtonDown   = Input.GetButton("Jump");
      bool DownKeyUp        = Input.GetKeyUp(KeyCode.DownArrow);
      bool UpKeyDown        = Input.GetKeyDown(KeyCode.UpArrow);

      if (_hp.Dead) { ActTransition("Die", animator); return; }

      if (_rigidState.Ground) {
        if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator);         return; }
        if (JumpButtonDown)                      { ActTransition("StepDownJump", animator); return; }
        if (DownKeyUp || UpKeyDown)              { ActTransition("Idle", animator);         return; }
      }

      if (_rigidState.Air) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("LieDown", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private PlayerHp _hp;
}

