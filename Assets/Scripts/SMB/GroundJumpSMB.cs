using UnityEngine;
using System.Collections;

public class GroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _skillInfo = animator.GetComponentInChildren<SkillInfo>();
      _jump = animator.GetComponent<GroundJump>();
      _health = animator.GetComponent<PlayerHealth>();
    }

    Debug.Log("jump");
    _jump.Jump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      SkillState stateX = _skillInfo.GetState(SkillName.X);
      SkillState stateShift = _skillInfo.GetState(SkillName.Shift);
      SkillState stateZ = _skillInfo.GetState(SkillName.Z);

      bool SkillFlag = (stateX == SkillState.Using) ||
                       (stateShift == SkillState.Using) ||
                       (stateZ == SkillState.Using);

      if (_health.Dead) { ActTransition("Die", animator); return; }

      if (SkillFlag) { ActTransition("Skill", animator); return; }

      if (_rigidState.Air) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("GroundJump", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private SkillInfo _skillInfo;
  private GroundJump _jump;
  private PlayerHealth _health;
}

