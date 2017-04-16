using UnityEngine;
using System.Collections;

public class FallSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _renderers = animator.GetComponentsInChildren<SpriteRenderer>();
      _airLinearMove = animator.GetComponent<AirLinearMove>();
    }

    Debug.Log("Fall");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
      bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
      bool JumpButtonDown   = Input.GetButton("Jump");
      bool LieDownFlag = OnlyDownKeyDown && !_rigidState.LadderTopEdge;
      bool ClimbFlag = OnlyUpKeyDown && !_rigidState.LadderTopEdge;
      bool SkillFlag = Input.GetKey(KeyCode.X) ||
                       Input.GetKey(KeyCode.LeftShift) ||
                       Input.GetKey(KeyCode.Z);

      if (SkillFlag) { ActTransition("Skill", animator); return; }

      if (OnlyLeftKeyDown)  { _airLinearMove.MoveLeft(); foreach (var sprite in _renderers) sprite.flipX = false; }
      if (OnlyRightKeyDown) { _airLinearMove.MoveRight(); foreach (var sprite in _renderers) sprite.flipX = true; }

      if (_rigidState.Ladder) {
        if (ClimbFlag) { ActTransition("Climb", animator); return; }
      }

      if (_rigidState.Ground) {
        if (OnlyLeftKeyDown || OnlyRightKeyDown) { ActTransition("Walk", animator);       return; }
        if (LieDownFlag)                         { ActTransition("LieDown", animator);    return; }
        if (JumpButtonDown)                      { ActTransition("GroundJump", animator); return; }
        ActTransition("Idle", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Fall", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private SpriteRenderer[] _renderers;
  private AirLinearMove _airLinearMove;
}

